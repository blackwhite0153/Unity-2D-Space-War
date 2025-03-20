using UnityEngine;

public class Missile : MonoBehaviour
{
    public float PlayerMissileSpeed = 5.0f;

    void Update()
    {
        MissileMove();
        Deactivate();
    }

    // 일정 Y 좌표 도달 시 비활성화
    private void Deactivate()
    {
        if (gameObject.transform.position.y >= 5.5f)
        {
            gameObject.SetActive(false);
        }
    }

    private void MissileMove()
    {
        transform.Translate(Vector2.up * PlayerMissileSpeed * Time.deltaTime, Space.World);
    }
}