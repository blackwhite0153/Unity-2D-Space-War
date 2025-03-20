using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TMP_Text BestScoreText;
    public TMP_Text ScoreText;

    public Button MainButton;
    public Button ReStartButton;
    public Button ExitButton;

    void Start()
    {
        Text();
        Button();
    }

    private void Text()
    {
        BestScoreText.text = $"Best Score : {Manager.Instance.BestScore}";
        ScoreText.text = $"Score : {Manager.Instance.Score}";

        Manager.Instance.Score = 0;
    }

    private void Button()
    {
        MainButton.onClick.AddListener(OnMainButtonClick);
        ReStartButton.onClick.AddListener(OnReStartButtonClick);
        ExitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnMainButtonClick()
    {
        SceneManager.LoadScene("Main");
    }

    private void OnReStartButtonClick()
    {
        SceneManager.LoadScene("Game");
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }
}