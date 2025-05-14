using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_GamePlay : UI_Base
{
    // 점수
    private int _score = 0;

    public TMP_Text ScoreText;

    // 일시정지 UI
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

    // 게임의 시간 흐름을 제어 (일시정지 및 해제)
    private void TimeScale()
    {
        if (Time.timeScale == 1.0f) Time.timeScale = 0.0f;  // 게임 정지
        else Time.timeScale = 1.0f; // 게임 진행
    }

    // 일시정지 버튼 클릭 시 실행
    private void OnPauseButtonClick()
    {
        TimeScale();

        // 일시정지 UI의 활성화 여부를 변경
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