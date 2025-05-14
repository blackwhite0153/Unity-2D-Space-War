using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileProcess : BaseMissileProcess
{
    private GameObject _player;

    // 빈 오브젝트
    private GameObject _playerMissilePool;

    private List<GameObject> PlayerMissileList = new List<GameObject>();

    // 발사 간격, 발사 쿨타임
    private float _interval;
    private float _coolTime;

    public GameObject PlayerMissilePrefab;

    private void Start()
    {
        Setting();
    }

    private void Update()
    {
        if (_player == null)
        {
            _player = FindAnyObjectByType<PlayerController>().gameObject;
        }
        else
        {
            Fire();
        }
    }

    private void Setting()
    {
        _playerMissilePool = new GameObject("PlayerMissilePool");

        _interval = 0.15f;
        _coolTime = 0.0f;
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
                AudioManager.Instance.PlaySFX("Laser");
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
                PlayerMissileList[i].transform.position = _player.transform.position;

                return PlayerMissileList[i];
            }
        }

        GameObject missile = Instantiate(PlayerMissilePrefab);

        // missile 오브젝트 PlayerMissilePool 하위에 할당
        missile.transform.parent = _playerMissilePool.transform;
        missile.transform.position = _player.transform.position;

        // 미사일 풀에 추가
        PlayerMissileList.Add(missile);

        return missile;
    }
}