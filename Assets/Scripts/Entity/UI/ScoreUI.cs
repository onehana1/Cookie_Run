using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : BaseUI
{
    TextMeshProUGUI currentScoreText;
    TextMeshProUGUI bestScoreText;

    protected override UIState GetUIState()
    {
        return UIState.Score;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        currentScoreText = transform.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>();
        bestScoreText = transform.Find("BestScoreText").GetComponent<TextMeshProUGUI>();
    }

    public void SetUI(int currentScore,int bestScore)
    {
        currentScoreText.text = currentScore.ToString();
        bestScoreText.text = bestScore.ToString();
    }
}