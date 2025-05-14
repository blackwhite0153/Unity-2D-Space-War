using UnityEngine;
using UnityEngine.UI;

public class BossController : BaseController
{
    private BossMissileProcess _bossMissileProcess;

    private Rigidbody2D _rigidbody2D;
    private PolygonCollider2D _polygonCollider2D;

    private Slider _bossHpBar;
    private Slider _playerHpBar;

    private bool _isDamageable;

    private float _moveSpeed;

    public GameObject EffectPrefab;

    protected override void Initialize()
    {
        Setting();
    }

    private void Start()
    {
        _bossHpBar.gameObject.SetActive(true);

        Navigation nav = _bossHpBar.navigation;
        nav.mode = Navigation.Mode.None;
        _bossHpBar.navigation = nav;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Setting()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _polygonCollider2D = GetComponent<PolygonCollider2D>();

        GameObject bossHpObject = GameObject.Find("GamePlay/UI_GamePlay").transform.Find("BossHpBar").gameObject;
        _bossHpBar = bossHpObject.GetComponent<Slider>();

        GameObject playerHpObject = GameObject.Find("GamePlay/UI_GamePlay").transform.Find("PlayerHpBar").gameObject;
        _playerHpBar = playerHpObject.GetComponent<Slider>();

        GameObject MissileProcess = GameObject.Find("Process").gameObject;
        _bossMissileProcess = MissileProcess.GetComponent<BossMissileProcess>();

        _rigidbody2D.gravityScale = 0.0f;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        _polygonCollider2D.isTrigger = true;

        Navigation nav = _bossHpBar.navigation;
        nav.mode = Navigation.Mode.None;
        _bossHpBar.navigation = nav;

        _bossHpBar.minValue = 0.0f;
        _bossHpBar.maxValue = 350.0f;

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));

        _isDamageable = false;

        _moveSpeed = 3.0f;
    }

    private void Move()
    {
        if (transform.position.y <= 4.5f)
        {
            if (!_isDamageable) Stop();

            return;
        }

        transform.Translate(Vector2.down * _moveSpeed * Time.deltaTime, Space.World);
    }

    private void Stop()
    {
        transform.position = new Vector2(0.0f, 4.5f);
        _rigidbody2D.linearVelocity = Vector2.zero;

        _isDamageable = true; // 도달한 이후부터 피격 허용

        _bossMissileProcess.Invoke("Think", 2.0f);
    }

    private void Effect()
    {
        GameObject effect = Instantiate(EffectPrefab);

        effect.transform.position = this.gameObject.transform.position - new Vector3(0, 0.01f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 보스가 정지했을 때부터 데미지를 입도록 설정
        if (!_isDamageable)
            return;

        // 플레이어 미사일에 피격 시
        if (collision.gameObject.CompareTag(Define.Player_Missile))
        {
            collision.gameObject.SetActive(false);

            // 체력 감소
            _bossHpBar.value -= 1;
            Effect();

            // 체력이 0 이하로 떨어질 시
            if (_bossHpBar.value <= 0)
            {
                GameManager.Instance.Score += 2000;
                GameManager.Instance.IsBossDie = true;
                _playerHpBar.value += 1.0f;

                _bossHpBar.gameObject.SetActive(false);
                _bossMissileProcess.CancelInvoke("Think");

                ObjectManager.Instance.CharacterDespawn(this);
            }
        }
    }
}