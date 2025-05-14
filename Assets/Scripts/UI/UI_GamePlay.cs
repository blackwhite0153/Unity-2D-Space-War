using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_GamePlay : UI_Base
{
    // ����
    private int _score = 0;

    public TMP_Text ScoreText;

    // �Ͻ����� UI
    public GameObject Pause;

    public Button PauseButton;
    public Button LobbyButton;

    protected override void Initialize()
    {
        base.Initialize();

        Setting();
    }

    private void Update()
    {
        DisplayScore();
    }

    private void Setting()
    {
        PauseButton.onClick.AddListener(OnPauseButtonClick);
        LobbyButton.onClick.AddListener(OnLobbyButtonClick);
    }

    // ������ �ð� �帧�� ���� (�Ͻ����� �� ����)
    private void TimeScale()
    {
        if (Time.timeScale == 1.0f) Time.timeScale = 0.0f;  // ���� ����
        else Time.timeScale = 1.0f; // ���� ����
    }

    // �Ͻ����� ��ư Ŭ�� �� ����
    private void OnPauseButtonClick()
    {
        TimeScale();

        // �Ͻ����� UI�� Ȱ��ȭ ���θ� ����
        if (Pause.activeSelf)
        {
            AudioManager.Instance.PauseOffBGM();
            Pause.SetActive(false);
        }
        else
        {
            AudioManager.Instance.PauseOnBGM();
            Pause.SetActive(true);
        }
    }

    private void OnLobbyButtonClick()
    {
        GameManager.Instance.IsGameOver = false;
        SceneManager.LoadScene("GameLobby");
    }

    public void DisplayScore()
    {
        _score = GameManager.Instance.Score;
        ScoreText.text = $"Score : {_score}";
    }
}