using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum UIState
{
    Score
}

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public event Action<bool> OnQuizModeChanged;    // ���� ���
    public event Action<bool> OnOXSelected; // ���� ���

    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    UIState currentState = UIState.Score;

    ScoreUI scoreUI = null;

    private void Awake()
    {
        instance = this;

        scoreUI = GetComponentInChildren<ScoreUI>(true);
        scoreUI?.Init(this);

        ChangeState(UIState.Score);
    }

    // UI ���� ����
    public void ChangeState(UIState state)
    {
        currentState = state;

        scoreUI?.SetActive(currentState);
    }
    public void OnClickExit()
    {

    }

    public void SetQuizMode(bool isQuizActive)
    {
        OnQuizModeChanged?.Invoke(isQuizActive);
    }
    public void SelectOXAnswer(bool isO)
    {
        OnOXSelected?.Invoke(isO); 
    }
}