using UnityEngine;

public class Background : MonoBehaviour
{
    // ��� ��ũ�� �ӵ�
    public float ScrollSpeed = 0.1f;

    // Quad�� Material�� ������ ����
    private Material m_Material;

    void Start()
    {
        // Renderer���� material ��������
        m_Material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Y �� �������� ��ũ��
        Vector2 offset = new Vector2(0, ScrollSpeed * Time.deltaTime);

        // Material�� ���� �ؽ��� ������ �� ����
        m_Material.mainTextureOffset += offset;
    }
}