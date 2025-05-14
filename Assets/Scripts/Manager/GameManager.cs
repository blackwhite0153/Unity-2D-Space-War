using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // 기본 상태 : 사망 (생성이 안되어있기에 사망 상태)
    private bool _isBossDie = true;
    // 기본 상태 : 게임 오버 상태가 아님
    private bool _isGameOver = false;

    // 점수
    private int _score = 0;

    public bool IsBossDie
    {
        get { return _isBossDie; }
        set { _isBossDie = value; }
    }

    public bool IsGameOver
    {
        get { return _isGameOver; }
        set { _isGameOver = value; }
    }

    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;

            if (_score > PlayerPrefs.GetInt("Score"))
            {
                PlayerPrefs.SetInt("Score", _score);
            }
        }
    }

    public int BestScore => PlayerPrefs.GetInt("Score");

    protected override void Initialize()
    {
        base.Initialize();
    }
}