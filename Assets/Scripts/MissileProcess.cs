using System.Collections.Generic;
using UnityEngine;

public class MissileProcess : MonoBehaviour
{
    public Transform Player;
    public GameObject PlayerMissilePrefab;

    private List<GameObject> PlayerMissileList = new List<GameObject>();

    // �� ������Ʈ
    private GameObject _playerMissilePool;

    // �߻� ����, �߻� ��Ÿ��
    private float _interval = 0.15f;
    private float _coolTime = 0.0f;

    void Start()
    {
        // PlayerMissilePool ������Ʈ ����
        _playerMissilePool = new GameObject("PlayerMissilePool");
    }

    void Update()
    {
        Fire();
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
                PlayerMissileList[i].transform.position = Player.transform.position;

                return PlayerMissileList[i];
            }
        }

        GameObject missile = Instantiate(PlayerMissilePrefab);

        // missile ������Ʈ PlayerMissilePool ������ �Ҵ�
        missile.transform.parent = _playerMissilePool.transform;
        missile.transform.position = Player.transform.position;

        // �̻��� Ǯ�� �߰�
        PlayerMissileList.Add(missile);

        return missile;
    }
}