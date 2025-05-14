using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_GameOver : UI_Base
{
    public TMP_Text BestScoreText;
    public TMP_Text ScoreText;

    public Button LobbyButton;
    public Button ReStartButton;
    public Button ExitButton;

    protected override void Initialize()
    {
        base.Initialize();

        Text();
        Button();
    }

    private void Text()
    {
        BestScoreText.text = $"Best Score : {GameManager.Instance.BestScore}";
        ScoreText.text = $"Score : {GameManager.Instance.Score}";

        GameManager.Instance.Score = 0;
    }

    private void Button()
    {
        LobbyButton.onClick.AddListener(OnMainButtonClick);
        ReStartButton.onClick.AddListener(OnReStartButtonClick);
        ExitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnMainButtonClick()
    {
        GameManager.Instance.IsGameOver = false;
        SceneManager.LoadScene("GameLobby");
    }

    private void OnReStartButtonClick()
    {
        GameManager.Instance.IsGameOver = false;
        SceneManager.LoadScene("GamePlay");
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }
}