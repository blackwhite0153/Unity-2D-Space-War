using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Á¡¼ö
    private int _score = 0;

    public TMP_Text ScoreText;

    void Update()
    {
        DisplayScore();
    }

    public void DisplayScore()
    {
        _score = Manager.Instance.Score;
        ScoreText.text = $"Score : {_score}";
    }
}