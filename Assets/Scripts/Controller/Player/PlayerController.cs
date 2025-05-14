using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : BaseController
{
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private CapsuleCollider2D _capsuleCollider2D;

    private Slider _playerHpBar;

    private float _moveSpeed;

    protected override void Initialize()
    {
        Setting();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Setting()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();

        GameObject playerHpObject = GameObject.Find("GamePlay/UI_GamePlay").transform.Find("PlayerHpBar").gameObject;
        _playerHpBar = playerHpObject.GetComponent<Slider>();

        Navigation nav = _playerHpBar.navigation;
        nav.mode = Navigation.Mode.None;
        _playerHpBar.navigation = nav;

        _rigidbody2D.gravityScale = 0.0f;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        _capsuleCollider2D.offset = new Vector2(0.01f, 0.0f);
        _capsuleCollider2D.size = new Vector2(0.2f, 0.1f);

        transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);

        _moveSpeed = 6.0f;
    }

    private void Move()
    {
        if (Input.GetButton(Define.Horizontal) || Input.GetButton(Define.Vertical))
        {
            float h = Input.GetAxisRaw(Define.Horizontal);
            float v = Input.GetAxisRaw(Define.Vertical);

            Vector2 movement = new Vector2(h, v);

            transform.Translate(movement.normalized * _moveSpeed * Time.deltaTime);

            // 이동 영역 제한
            Vector3 currentPos = transform.position;
            currentPos.x = Mathf.Clamp(transform.position.x, -8.5f, 8.5f);
            currentPos.y = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
            transform.position = currentPos;
        }
    }

    // 피격 효과
    private IEnumerator CoDamaged()
    {
        for (int i = 0; i < 4; i++)
        {
            _spriteRenderer.color = new Color(255, 255, 255, 0);
            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.color = new Color(255, 255, 255, 100);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemy, Enemy_Missile 에 접촉 시 체력 감소
        if (collision.gameObject.CompareTag(Define.Enemy))
        {
            StartCoroutine(CoDamaged());
            _playerHpBar.value -= 1.0f;

            if (_playerHpBar.value <= 0)
            {
                GameManager.Instance.IsBossDie = true;
                GameManager.Instance.IsGameOver = true;

                if (GameManager.Instance.IsGameOver) SceneManager.LoadScene("GameOver");
            }
        }

        if (collision.gameObject.CompareTag(Define.Boss) || collision.gameObject.CompareTag(Define.Boss_Missile))
        {
            StartCoroutine(CoDamaged());
            _playerHpBar.value -= 1.0f;

            if (collision.gameObject.CompareTag(Define.Boss_Missile))
            {
                collision.gameObject.SetActive(false);
            }

            if (_playerHpBar.value <= 0)
            {
                GameManager.Instance.IsBossDie = true;
                GameManager.Instance.IsGameOver = true;

                if (GameManager.Instance.IsGameOver) SceneManager.LoadScene("GameOver");
            }
        }
    }
}