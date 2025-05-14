using UnityEngine;
using UnityEngine.UI;

public class UI_Base : MonoBehaviour
{
    private void Awake()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        SetCanvas();
    }

    private void SetCanvas()
    {
        // ���� GameObject�� Canvas ������Ʈ�� �������ų� ������ �߰�
        Canvas canvas = gameObject.GetOrAddComponent<Canvas>();

        // Canvas�� �����ϴ� ��� ���� ����
        if (canvas != null)
        {
            // UI�� ȭ�� ��ǥ �������� ������
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            // ���� �켱 ������ �������� ���� �����ϵ��� ����
            canvas.overrideSorting = true;
        }

        // ���� GameObject�� CanvasScaler ������Ʈ�� �������ų� ������ �߰�
        CanvasScaler canvasScaler = gameObject.GetOrAddComponent<CanvasScaler>();
        // UI Scale Mode�� Scale With Screen Size�� ����
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        // Reference Resolution�� 720p (1280 x 720) ũ��� ����
        canvasScaler.referenceResolution = new Vector2(1280.0f, 720.0f);
    }
}