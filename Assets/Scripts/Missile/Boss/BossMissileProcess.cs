using System.Collections.Generic;
using UnityEngine;

public class BossMissileProcess : BaseMissileProcess
{
    private GameObject _player;
    private GameObject _boss;

    // 빈 오브젝트
    private GameObject _bossMissilePool;

    private List<GameObject> BossMissileList = new List<GameObject>();

    private int _patternIndex;
    private int _currentPatternCount;
    private int[] _maxPatternCount;

    public GameObject BossMissilePrefab;

    private void Start()
    {
        Setting();
    }

    private void Update()
    {
        if (_player == null)
        {
            var playerController = FindAnyObjectByType<PlayerController>();

            if (playerController != null)
            {
                _player = playerController.gameObject;
            }
        }
        if (_boss == null)
        {
            var bossController = FindAnyObjectByType<BossController>();

            if (bossController != null)
            {
                _boss = bossController.gameObject;
            }
        }
        if (_boss != null && !_boss.activeSelf)
        {
            _boss = null;
        }
    }

    private void Setting()
    {
        // BossMissilePool 오브젝트 생성
        _bossMissilePool = new GameObject("BossMissilePool");

        _patternIndex = 3;
        _currentPatternCount = 0;
        _maxPatternCount = new int[4];

        _maxPatternCount[0] = 2;
        _maxPatternCount[1] = 3;
        _maxPatternCount[2] = 21;
        _maxPatternCount[3] = 10;
    }

    private void Think()
    {
        _patternIndex = _patternIndex == 3 ? 0 : _patternIndex + 1;
        _currentPatternCount = 0;

        switch (_patternIndex)
        {
            case 0:
                FireFoward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;
        }
    }

    // 정면 4발 발사
    private void FireFoward()
    {
        if (GameManager.Instance.IsBossDie) return;

        GameObject bulletR = GetMissilePool();
        GameObject bulletRR = GetMissilePool();
        GameObject bulletL = GetMissilePool();
        GameObject bulletLL = GetMissilePool();

        bulletR.transform.position = _boss.transform.position + Vector3.right * 0.4f;
        bulletRR.transform.position = _boss.transform.position + Vector3.right * 1.0f;
        bulletL.transform.position = _boss.transform.position + Vector3.left * 0.4f;
        bulletLL.transform.position = _boss.transform.position + Vector3.left * 1.0f;

        Rigidbody2D rigidbody2DR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidbody2DRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidbody2DL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidbody2DLL = bulletLL.GetComponent<Rigidbody2D>();

        rigidbody2DR.AddForce(Vector2.down * 8.0f, ForceMode2D.Impulse);
        rigidbody2DRR.AddForce(Vector2.down * 8.0f, ForceMode2D.Impulse);
        rigidbody2DL.AddForce(Vector2.down * 8.0f, ForceMode2D.Impulse);
        rigidbody2DLL.AddForce(Vector2.down * 8.0f, ForceMode2D.Impulse);

        _currentPatternCount++;

        if (_currentPatternCount < _maxPatternCount[_patternIndex])
            Invoke("FireFoward", 2.0f);
        else
            Invoke("Think", 3.0f);
    }

    // 플레이어 방향으로 샷건
    private void FireShot()
    {
        if (GameManager.Instance.IsBossDie) return;

        for (int index = 0; index < 8; index++)
        {
            GameObject bullet = GetMissilePool();
            bullet.transform.position = _boss.transform.position;

            Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();
            
            Vector2 directionVector = _player.transform.position - _boss.transform.position;
            Vector2 randomVector = new Vector2(Random.Range(-2.0f, 2.0f), Random.Range(0.0f, 3.0f));

            directionVector += randomVector;

            rigidbody2D.AddForce(directionVector.normalized * 6.5f, ForceMode2D.Impulse);
        }

        _currentPatternCount++;

        if (_currentPatternCount < _maxPatternCount[_patternIndex])
            Invoke("FireShot", 3.5f);
        else
            Invoke("Think", 3.0f);
    }

    // 부채 모양으로 발사
    private void FireArc()
    {
        if (GameManager.Instance.IsBossDie) return;

        GameObject bullet = GetMissilePool();
        bullet.transform.position = _boss.transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();

        float angle = Mathf.Lerp(-160.0f, -20.0f, (float)_currentPatternCount / (_maxPatternCount[_patternIndex] - 1));
        float rad = angle * Mathf.Deg2Rad;
        Vector2 directionVector = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        rigidbody2D.AddForce(directionVector.normalized * 5.0f, ForceMode2D.Impulse);

        _currentPatternCount++;

        if (_currentPatternCount < _maxPatternCount[_patternIndex])
            Invoke("FireArc", 0.15f);
        else
            Invoke("Think", 3.0f);
    }

    // 원 형태로 전체 공격
    private void FireAround()
    {
        if (GameManager.Instance.IsBossDie) return;

        int roundNumA = 40;
        int roundNumB = 30;
        int roundNum = _currentPatternCount % 2 == 0 ? roundNumA : roundNumB;

        for (int index = 0; index < roundNum; index++)
        {
            GameObject bullet = GetMissilePool();
            bullet.transform.position = _boss.transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();
            Vector2 directionVector = new Vector2(Mathf.Cos(Mathf.PI * 2.0f * index / roundNum),
                                                  Mathf.Sin(Mathf.PI * 2.0f * index / roundNum));

            rigidbody2D.AddForce(directionVector.normalized * 3.5f, ForceMode2D.Impulse);

            Vector3 rotationVector = Vector3.forward * 360.0f * index / roundNum + Vector3.forward * 90.0f;
            bullet.transform.Rotate(rotationVector);
        }

        _currentPatternCount++;

        if (_currentPatternCount < _maxPatternCount[_patternIndex])
            Invoke("FireAround", 0.7f);
        else
            Invoke("Think", 3.0f);
    }

    private GameObject GetMissilePool()
    {
        // 비활성화 미사일 체크
        for (int i = 0; i < BossMissileList.Count; i++)
        {
            if (BossMissileList[i].activeSelf == false)
            {
                BossMissileList[i].SetActive(true);
                BossMissileList[i].transform.position = _boss.transform.position;

                return BossMissileList[i];
            }
        }

        GameObject missile = Instantiate(BossMissilePrefab);

        // missile 오브젝트 BossMissilePool 하위에 할당
        missile.transform.parent = _bossMissilePool.transform;
        missile.transform.position = _boss.transform.position;

        // 미사일 풀에 추가
        BossMissileList.Add(missile);

        return missile;
    }
}