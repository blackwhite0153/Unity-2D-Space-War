using System.Collections;
using UnityEngine;

public class Boss_Missile : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(CoDeactivate());
    }

    // ���� Y ��ǥ ���� �� ��Ȱ��ȭ
    private IEnumerator CoDeactivate()
    {
        yield return new WaitForSeconds(6.0f);
        Destroy(gameObject);
    }
}