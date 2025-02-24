using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum UIState
{
    Score
}

public class UIManager : MonoBehaviour
{
    static UIManager instance;

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
}