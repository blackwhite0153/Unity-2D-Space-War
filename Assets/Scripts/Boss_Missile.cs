using System.Collections;
using UnityEngine;

public class Boss_Missile : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(CoDeactivate());
    }

    // 일정 Y 좌표 도달 시 비활성화
    private IEnumerator CoDeactivate()
    {
        yield return new WaitForSeconds(6.0f);
        Destroy(gameObject);
    }
}