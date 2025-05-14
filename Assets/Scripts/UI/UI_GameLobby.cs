using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_GameLobby : UI_Base
{
    public Button GameStartButton;
    public Button ExitButton;

    protected override void Initialize()
    {
        base.Initialize();

        Setting();
    }

    private void Setting()
    {
        AudioManager.Instance.PlayBGM("Arcade Game");

        GameStartButton.onClick.AddListener(GameStartButtonClick);
        ExitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void GameStartButtonClick()
    {
        SceneManager.LoadScene("GamePlay");
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }
}