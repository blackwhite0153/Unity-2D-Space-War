using System.Collections;
using UnityEngine;

public class BossMissile : BaseMissile
{
    private Coroutine _coMissileDestroy;

    private Rigidbody2D _rigidbody2D;
    private CircleCollider2D _circleCollider2D;

    protected override void Initialize()
    {
        base.Initialize();

        Setting();
    }

    private void OnEnable()
    {
        if (_coMissileDestroy == null)
        {
            _coMissileDestroy = StartCoroutine(CoDeactivate());
        }
    }

    private void OnDisable()
    {
        if (_coMissileDestroy != null)
        {
            _coMissileDestroy = null;
        }
    }

    private void Setting()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _circleCollider2D = GetComponent<CircleCollider2D>();

        _rigidbody2D.gravityScale = 0.0f;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        _circleCollider2D.isTrigger = true;
        _circleCollider2D.radius = 0.125f;

        _coMissileDestroy = null;
    }

    // 일정 시간이 지나면 비활성화
    private IEnumerator CoDeactivate()
    {
        yield return new WaitForSeconds(4.5f);

        ObjectManager.Instance.MissileDespawn(this);
    }
}