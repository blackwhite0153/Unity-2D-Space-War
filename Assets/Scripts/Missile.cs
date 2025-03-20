using UnityEngine;

public class Missile : MonoBehaviour
{
    public float PlayerMissileSpeed = 5.0f;

    void Update()
    {
        MissileMove();
        Deactivate();
    }

    // ���� Y ��ǥ ���� �� ��Ȱ��ȭ
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