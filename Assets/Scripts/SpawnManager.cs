using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // 생성될 Enemy 프리팹
    public GameObject EnemyRedPrefab;
    public GameObject EnemyGreenPrefab;
    public GameObject EnemyBluePrefab;

    // Enemy 프리팹을 담을 _enemyObjects 변수
    private GameObject[] _enemyObjects;

    private float _spawnInterval = 1.5f;    // 오브젝트 생성 간격
    private float _createY = 5.5f;          // 오브젝트 생성 Y 좌표

    void Start()
    {
        // 프리팹 할당
        _enemyObjects = new GameObject[3];
        _enemyObjects[0] = EnemyRedPrefab;
        _enemyObjects[1] = EnemyGreenPrefab;
        _enemyObjects[2] = EnemyBluePrefab;

        StartCoroutine(CoCreateEnemy());
    }

    // 적 오브젝트 생성
    private IEnumerator CoCreateEnemy()
    {
        for (int i = 0; i < 3; i++)
        {
            // 생성될 랜덤 Enemy
            int enemyRandom = Random.Range(0, 3);
            // 생성될 랜덤 X 좌표
            float xPosRandom = Random.Range(-8.5f, 8.5f);

            // 생성될 좌표 설정
            Vector2 spawnPos = new Vector2(xPosRandom, _createY);

            // Enemy 오브젝트 생성
            Instantiate(_enemyObjects[enemyRandom], spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(0.05f);
        }

        // 생성 간격
        yield return new WaitForSeconds(_spawnInterval);

        StartCoroutine(CoCreateEnemy());
    }
}