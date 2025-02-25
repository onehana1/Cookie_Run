using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : BaseUI
{
    TextMeshProUGUI currentScoreText;
    TextMeshProUGUI bestScoreText;
    PlayManager playManager;

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

    public void SetUI(int currentScore, int bestScore)
    {
        playManager.AddScore(currentScore);
        currentScoreText.text = playManager.score.ToString();
        bestScoreText.text = bestScore.ToString();
    }
}