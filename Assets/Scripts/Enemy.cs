using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject EffectPrefab;

    public int EnemyHp = 3;
    public float moveSpeed = 1.5f;

    void Update()
    {
        Move();
        EnemyDestroy();
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
                Manager.Instance.Score += 200;
                Destroy(this.gameObject);
            }
        }
    }
}