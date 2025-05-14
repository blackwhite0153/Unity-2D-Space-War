using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileProcess : BaseMissileProcess
{
    private GameObject _player;

    // �� ������Ʈ
    private GameObject _playerMissilePool;

    private List<GameObject> PlayerMissileList = new List<GameObject>();

    // �߻� ����, �߻� ��Ÿ��
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

    // �̻��� �߻�
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
        // ��Ȱ��ȭ �̻��� üũ
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

        // missile ������Ʈ PlayerMissilePool ������ �Ҵ�
        missile.transform.parent = _playerMissilePool.transform;
        missile.transform.position = _player.transform.position;

        // �̻��� Ǯ�� �߰�
        PlayerMissileList.Add(missile);

        return missile;
    }
}