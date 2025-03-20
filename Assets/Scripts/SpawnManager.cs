using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // ������ Enemy ������
    public GameObject EnemyRedPrefab;
    public GameObject EnemyGreenPrefab;
    public GameObject EnemyBluePrefab;

    // Enemy �������� ���� _enemyObjects ����
    private GameObject[] _enemyObjects;

    private float _spawnInterval = 1.5f;    // ������Ʈ ���� ����
    private float _createY = 5.5f;          // ������Ʈ ���� Y ��ǥ

    void Start()
    {
        // ������ �Ҵ�
        _enemyObjects = new GameObject[3];
        _enemyObjects[0] = EnemyRedPrefab;
        _enemyObjects[1] = EnemyGreenPrefab;
        _enemyObjects[2] = EnemyBluePrefab;

        StartCoroutine(CoCreateEnemy());
    }

    // �� ������Ʈ ����
    private IEnumerator CoCreateEnemy()
    {
        for (int i = 0; i < 3; i++)
        {
            // ������ ���� Enemy
            int enemyRandom = Random.Range(0, 3);
            // ������ ���� X ��ǥ
            float xPosRandom = Random.Range(-8.5f, 8.5f);

            // ������ ��ǥ ����
            Vector2 spawnPos = new Vector2(xPosRandom, _createY);

            // Enemy ������Ʈ ����
            Instantiate(_enemyObjects[enemyRandom], spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(0.05f);
        }

        // ���� ����
        yield return new WaitForSeconds(_spawnInterval);

        StartCoroutine(CoCreateEnemy());
    }
}