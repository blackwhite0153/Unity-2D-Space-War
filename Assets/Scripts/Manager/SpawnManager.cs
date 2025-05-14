using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Enemy �������� ���� _enemyObjects ����
    private GameObject[] _enemyObjects;

    private Coroutine _coEnemySpawn;
    private Coroutine _coBossSpawn;

    private WaitForSeconds _enemySpawnDelay;
    private WaitForSeconds _bossSpawnDelay;

    private float _createY = 5.5f;  // ������Ʈ ���� Y ��ǥ

    // ���� ������
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
        // ���� ������Ʈ ���ҽ� �ε�
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

    // �÷��̾� ����
    private void PlayerSpawn()
    {
        ObjectManager.Instance.CharacterSpawn<PlayerController>(new Vector3(0.0f, -4.0f, 0.0f));
    }

    // �� ������Ʈ ����
    private IEnumerator CoCreateEnemy()
    {
        while (true)
        {
            // ������ ���� X ��ǥ
            float xPosRandom = Random.Range(-8.5f, 8.5f);

            // ������ ��ǥ ����
            Vector2 spawnPos = new Vector2(xPosRandom, _createY);

            // Enemy ������Ʈ ����
            PoolManager.Instance.GetCharacterObject<EnemyController>(spawnPos);

            // ���� ����
            yield return _enemySpawnDelay;
        }
    }

    // 30�ʸ��� ���� ����
    private IEnumerator CoBossSpawn()
    {
        while (true)
        {
            // ������ ����ִ� ���� ��ٸ�
            yield return new WaitUntil(() => GameManager.Instance.IsBossDie == true);

            // ���� ��� �� 30�� ���
            yield return _bossSpawnDelay;

            // ���� ����
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