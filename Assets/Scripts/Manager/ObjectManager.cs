using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    // �÷��̾� ������
    private GameObject _playerResource;

    // Boss ������
    private GameObject _bossResource;

    // Enemy ������
    private List<GameObject> _enemyResource;

    // �� ��ü���� �����ϴ� HashSet
    private HashSet<EnemyController> Enemy { get; set; } = new HashSet<EnemyController>();

    protected override void Initialize()
    {
        base.Initialize();

        Setting();
    }

    private void Setting()
    {
        _enemyResource = new List<GameObject>();
    }

    // ��� ���� ������Ʈ ���ҽ��� �ε��ϴ� �Լ�
    public void ResourceAllLoad()
    {
        // Resources.Load<T>(Path)�� ����Ͽ� ������ �ε�
        _playerResource = Resources.Load<GameObject>(Define.PlayerPath);

        _bossResource = Resources.Load<GameObject>(Define.BossPath);

        _enemyResource.Add(Resources.Load<GameObject>(Define.EnemyRedPath));
        _enemyResource.Add(Resources.Load<GameObject>(Define.EnemyGreenPath));
        _enemyResource.Add(Resources.Load<GameObject>(Define.EnemyBluePath));
    }

    public T CharacterSpawn<T>(Vector3 spawnPos) where T : BaseController
    {
        Type type = typeof(T);

        if (type == typeof(PlayerController))
        {
            GameObject obj = Instantiate(_playerResource, spawnPos, Quaternion.identity);   // ������Ʈ ����
            PlayerController playerController = obj.GetOrAddComponent<PlayerController>();  // PlayerController ������Ʈ �߰�

            // ���׸� Ÿ�� T�� ĳ�����Ͽ� ��ȯ
            return playerController as T;
        }
        else if (type == typeof(BossController))
        {
            GameObject obj = Instantiate(_bossResource, spawnPos, Quaternion.identity); // ������Ʈ ����
            BossController bossController = obj.GetOrAddComponent<BossController>();    // BossController ������Ʈ �߰�

            // ���׸� Ÿ�� T�� ĳ�����Ͽ� ��ȯ
            return bossController as T;
        }
        else if (type == typeof(EnemyController))
        {
            int random = UnityEngine.Random.Range(0, 3);

            GameObject obj = GameObject.Instantiate(_enemyResource[random], spawnPos, Quaternion.identity); // ������Ʈ ����
            EnemyController enemyController = obj.GetOrAddComponent<EnemyController>(); // EnemyController ������Ʈ �߰�

            Enemy.Add(enemyController); // ������ Enemy�� HashSet�� �߰�

            // ���׸� Ÿ�� T�� ĳ�����Ͽ� ��ȯ
            return enemyController as T;
        }
            return null;
    }

    public void CharacterDespawn<T>(T obj) where T : BaseController
    {
        obj.gameObject.SetActive(false);
    }

    public void MissileDespawn<T>(T obj) where T : BaseMissile
    {
        obj.gameObject.SetActive(false);
    }

    protected override void Clear()
    {
        base.Clear();
    }
}