using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private SpriteRenderer _renderer;

    private int _tempScore = 1000;

    public Slider PlayerHpBar;

    public float MoveSpeed = 6.0f;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        HpController();
    }

    private void Move()
    {
        float h = Input.GetAxisRaw(Define.Horizontal);
        float v = Input.GetAxisRaw(Define.Vertical);

        Vector2 movement = new Vector2(h, v);

        transform.Translate(movement.normalized * MoveSpeed * Time.deltaTime);

        // 이동 영역 제한
        Vector3 currentPos = transform.position;
        currentPos.x = Mathf.Clamp(transform.position.x, -8.5f, 8.5f);
        currentPos.y = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
        transform.position = currentPos;
    }

    private void HpController()
    {
        if (_tempScore * 2 <= Manager.Instance.Score)
        {
            PlayerHpBar.value++;
            _tempScore = Manager.Instance.Score;
        }
    }

    // 피격 효과
    private IEnumerator CoDamaged()
    {
        for (int i = 0; i < 4; i++)
        {
            _renderer.color = new Color(255, 255, 255, 0);
            yield return new WaitForSeconds(0.1f);
            _renderer.color = new Color(255, 255, 255, 100);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemy, Enemy_Missile 에 접촉 시 체력 감소
        if (collision.gameObject.CompareTag(Define.Enemy) || collision.gameObject.CompareTag(Define.Enemy_Missile))
        {
            StartCoroutine(CoDamaged());
            PlayerHpBar.value--;

            if (PlayerHpBar.value <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}