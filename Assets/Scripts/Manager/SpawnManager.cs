using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Enemy 프리팹을 담을 _enemyObjects 변수
    private GameObject[] _enemyObjects;

    private Coroutine _coEnemySpawn;
    private Coroutine _coBossSpawn;

    private WaitForSeconds _enemySpawnDelay;
    private WaitForSeconds _bossSpawnDelay;

    private float _createY = 5.5f;  // 오브젝트 생성 Y 좌표

    // 보스 프리팹
    public GameObject BossPrefab;

    private void Start()
    {
        Setting();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver)
        {
            Clear();
        }
    }

    private void Setting()
    {
        // 게임 오브젝트 리소스 로드
        ObjectManager.Instance.ResourceAllLoad();

        _coEnemySpawn = null;
        _coBossSpawn = null;

        _enemySpawnDelay = new WaitForSeconds(Random.Range(0.2f, 0.5f));
        _bossSpawnDelay = new WaitForSeconds(30.0f);

        PlayerSpawn();

        if (_coEnemySpawn == null)
        {
            _coEnemySpawn = StartCoroutine(CoCreateEnemy());
            _coEnemySpawn = null;
        }
        if (_coBossSpawn == null)
        {
            _coBossSpawn = StartCoroutine(CoBossSpawn());
            _coBossSpawn = null;
        }
    }

    // 플레이어 생성
    private void PlayerSpawn()
    {
        ObjectManager.Instance.CharacterSpawn<PlayerController>(new Vector3(0.0f, -4.0f, 0.0f));
    }

    // 적 오브젝트 생성
    private IEnumerator CoCreateEnemy()
    {
        while (true)
        {
            // 생성될 랜덤 X 좌표
            float xPosRandom = Random.Range(-8.5f, 8.5f);

            // 생성될 좌표 설정
            Vector2 spawnPos = new Vector2(xPosRandom, _createY);

            // Enemy 오브젝트 생성
            PoolManager.Instance.GetCharacterObject<EnemyController>(spawnPos);

            // 생성 간격
            yield return _enemySpawnDelay;
        }
    }

    // 30초마다 보스 생성
    private IEnumerator CoBossSpawn()
    {
        while (true)
        {
            // 보스가 살아있는 동안 기다림
            yield return new WaitUntil(() => GameManager.Instance.IsBossDie == true);

            // 보스 사망 후 30초 대기
            yield return _bossSpawnDelay;

            // 보스 생성
            PoolManager.Instance.GetCharacterObject<BossController>(new Vector3(0.0f, 10.0f, 0.0f));
            GameManager.Instance.IsBossDie = false;
        }
    }

    private void Clear()
    {
        if (_coEnemySpawn != null)
        {
            StopCoroutine(_coEnemySpawn);
        }
        if (_coBossSpawn != null)
        {
            StopCoroutine(_coBossSpawn);
        }
    }
}