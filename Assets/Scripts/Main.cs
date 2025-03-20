using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public Button GameStartButton;
    public Button ExitButton;

    void Start()
    {
        GameStartButton.onClick.AddListener(GameStartButtonClick);
        ExitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void GameStartButtonClick()
    {
        SceneManager.LoadScene("Game");
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }
}