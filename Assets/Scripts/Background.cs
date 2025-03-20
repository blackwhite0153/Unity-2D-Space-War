using UnityEngine;

public class Background : MonoBehaviour
{
    // 배경 스크롤 속도
    public float ScrollSpeed = 0.1f;

    // Quad의 Material을 저장할 변수
    private Material m_Material;

    void Start()
    {
        // Renderer에서 material 가져오기
        m_Material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Y 축 방향으로 스크롤
        Vector2 offset = new Vector2(0, ScrollSpeed * Time.deltaTime);

        // Material의 메인 텍스쳐 오프셋 값 변경
        m_Material.mainTextureOffset += offset;
    }
}