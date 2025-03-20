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

    // ���� Y ��ǥ ���� �� ����
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
        // �÷��̾� �̻��Ͽ� �ǰ� ��
        if (collision.gameObject.CompareTag(Define.Player_Missile))
        {
            // ü�� ����
            EnemyHp--;
            Effect();

            collision.gameObject.SetActive(false);

            // ü���� 0 ���Ϸ� ������ ��
            if (EnemyHp <= 0)
            {
                Manager.Instance.Score += 200;
                Destroy(this.gameObject);
            }
        }
    }
}