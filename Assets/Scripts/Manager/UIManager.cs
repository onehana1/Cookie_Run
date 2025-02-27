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
    public event Action<bool> OnQuizModeChanged;    // 퀴즈 모드
    public event Action<bool> OnOXSelected; // 퀴즈 모드

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

    // UI 상태 변경
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