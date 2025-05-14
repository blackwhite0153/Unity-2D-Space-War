using UnityEngine;

public class PlayerMissile : BaseMissile
{
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;

    public float PlayerMissileSpeed = 5.0f;

    protected override void Initialize()
    {
        base.Initialize();

        Setting();
    }

    private void FixedUpdate()
    {
        MissileMove();
    }

    private void Update()
    {
        Deactivate();
    }

    private void Setting()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();

        _rigidbody2D.gravityScale = 0.0f;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        _boxCollider2D.isTrigger = true;
        _boxCollider2D.offset = new Vector2(-0.01f, -0.01f);
        _boxCollider2D.size = new Vector2(0.6f, 0.15f);

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));
    }

    private void MissileMove()
    {
        transform.Translate(Vector2.up * PlayerMissileSpeed * Time.deltaTime, Space.World);
    }

    // 일정 Y 좌표 도달 시 비활성화
    private void Deactivate()
    {
        if (gameObject.transform.position.y >= 5.5f)
        {
            ObjectManager.Instance.MissileDespawn(this);
        }
    }
}