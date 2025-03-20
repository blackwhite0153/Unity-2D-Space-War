using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public GameObject EffectPrefab;
    public GameObject BossMissilePrefab;

    public Slider BossHpBar;

    void Start()
    {
        StartCoroutine(FireAround());
    }

    private IEnumerator FireAround()
    {
        for (int i = 0; i <= 360; i += 10)
        {
            GameObject missile = Instantiate(BossMissilePrefab, transform, true);
            Vector2 direction = new Vector2(Mathf.Cos(i * Mathf.Deg2Rad), Mathf.Sin(i * Mathf.Deg2Rad));

            missile.transform.right = direction;
            missile.transform.position = transform.position;
        }

        yield return new WaitForSeconds(5.0f);

        StartCoroutine(FireAround());
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
            BossHpBar.value--;
            Effect();

            collision.gameObject.SetActive(false);

            // ü���� 0 ���Ϸ� ������ ��
            if (BossHpBar.value <= 0)
            {
                Manager.Instance.Score += 1000;
                Destroy(this.gameObject);
            }
        }
    }
}