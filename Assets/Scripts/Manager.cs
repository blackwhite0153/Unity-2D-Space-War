using TMPro;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // GameManager 싱글톤 인스턴스
    private static Manager s_instance = null;

    private int _score = 0;

    public static Manager Instance
    {
        get
        {
            if (s_instance == null)
                return null;
            return s_instance;
        }
    }

    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;

            Debug.Log($"{_score}, {PlayerPrefs.GetInt("Score")}");

            if (_score > PlayerPrefs.GetInt("Score"))
            {
                PlayerPrefs.SetInt("Score", _score);
            }
        }
    }

    public int BestScore => PlayerPrefs.GetInt("Score");

    void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
        // 값이 있다면
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}