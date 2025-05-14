using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    // 플레이어 프리팹
    private GameObject _playerResource;

    // Boss 프리팹
    private GameObject _bossResource;

    // Enemy 프리팹
    private List<GameObject> _enemyResource;

    // 적 객체들을 관리하는 HashSet
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

    // 모든 게임 오브젝트 리소스를 로드하는 함수
    public void ResourceAllLoad()
    {
        // Resources.Load<T>(Path)를 사용하여 프리팹 로드
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
            GameObject obj = Instantiate(_playerResource, spawnPos, Quaternion.identity);   // 오브젝트 생성
            PlayerController playerController = obj.GetOrAddComponent<PlayerController>();  // PlayerController 컴포넌트 추가

            // 제네릭 타입 T로 캐스팅하여 반환
            return playerController as T;
        }
        else if (type == typeof(BossController))
        {
            GameObject obj = Instantiate(_bossResource, spawnPos, Quaternion.identity); // 오브젝트 생성
            BossController bossController = obj.GetOrAddComponent<BossController>();    // BossController 컴포넌트 추가

            // 제네릭 타입 T로 캐스팅하여 반환
            return bossController as T;
        }
        else if (type == typeof(EnemyController))
        {
            int random = UnityEngine.Random.Range(0, 3);

            GameObject obj = GameObject.Instantiate(_enemyResource[random], spawnPos, Quaternion.identity); // 오브젝트 생성
            EnemyController enemyController = obj.GetOrAddComponent<EnemyController>(); // EnemyController 컴포넌트 추가

            Enemy.Add(enemyController); // 생성된 Enemy를 HashSet에 추가

            // 제네릭 타입 T로 캐스팅하여 반환
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