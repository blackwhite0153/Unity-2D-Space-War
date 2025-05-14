using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // �⺻ ���� : ��� (������ �ȵǾ��ֱ⿡ ��� ����)
    private bool _isBossDie = true;
    // �⺻ ���� : ���� ���� ���°� �ƴ�
    private bool _isGameOver = false;

    // ����
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