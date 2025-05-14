using UnityEngine;
using UnityEngine.UI;

public class EnemyController : BaseController
{
    private Rigidbody2D _rigidbody2D;
    private CircleCollider2D _circleCollider2D;

    private Slider _playerHpBar;

    public GameObject EffectPrefab;

    public int EnemyHp = 5;
    public float moveSpeed = 1.5f;

    protected override void Initialize()
    {
        Setting();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        EnemyDestroy();
    }

    private void Setting()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _circleCollider2D = GetComponent<CircleCollider2D>();

        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        _rigidbody2D.freezeRotation = true;

        _circleCollider2D.isTrigger = true;
        _circleCollider2D.radius = 0.15f;

        GameObject playerHpObject = GameObject.Find("GamePlay/UI_GamePlay").transform.Find("PlayerHpBar").gameObject;
        _playerHpBar = playerHpObject.GetComponent<Slider>();
    }

    private void Move()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }

    // 일정 Y 좌표 도달 시 제거
    private void EnemyDestroy()
    {
        if (this.gameObject.transform.position.y <= -5.5f)
        {
            Destroy(gameObject);
        }
    }

    private void Effect()
    {
        GameObject effect = Instantiate(EffectPrefab);

        effect.transform.position = this.gameObject.transform.position - new Vector3(0, 0.01f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어 미사일에 피격 시
        if (collision.gameObject.CompareTag(Define.Player_Missile))
        {
            // 체력 감소
            EnemyHp--;
            Effect();

            collision.gameObject.SetActive(false);

            // 체력이 0 이하로 떨어질 시
            if (EnemyHp <= 0)
            {
                GameManager.Instance.Score += 150;
                _playerHpBar.value += 0.1f;

                ObjectManager.Instance.CharacterDespawn(this);
            }
        }
    }
}