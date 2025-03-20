using System.Collections.Generic;
using UnityEngine;

public class MissileProcess : MonoBehaviour
{
    public Transform Player;
    public GameObject PlayerMissilePrefab;

    private List<GameObject> PlayerMissileList = new List<GameObject>();

    // 빈 오브젝트
    private GameObject _playerMissilePool;

    // 발사 간격, 발사 쿨타임
    private float _interval = 0.15f;
    private float _coolTime = 0.0f;

    void Start()
    {
        // PlayerMissilePool 오브젝트 생성
        _playerMissilePool = new GameObject("PlayerMissilePool");
    }

    void Update()
    {
        Fire();
    }

    // 미사일 발사
    private void Fire()
    {
        _coolTime += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            if (_interval < _coolTime)
            {
                _coolTime = 0.0f;
                GetMissilePool();
            }
        }
    }

    private GameObject GetMissilePool()
    {
        // 비활성화 미사일 체크
        for (int i = 0; i < PlayerMissileList.Count; i++)
        {
            if (PlayerMissileList[i].activeSelf == false)
            {
                PlayerMissileList[i].SetActive(true);
                PlayerMissileList[i].transform.position = Player.transform.position;

                return PlayerMissileList[i];
            }
        }

        GameObject missile = Instantiate(PlayerMissilePrefab);

        // missile 오브젝트 PlayerMissilePool 하위에 할당
        missile.transform.parent = _playerMissilePool.transform;
        missile.transform.position = Player.transform.position;

        // 미사일 풀에 추가
        PlayerMissileList.Add(missile);

        return missile;
    }
}