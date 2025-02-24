using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : BaseUI
{
    TextMeshProUGUI currentGoldText;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        currentGoldText = transform.Find("CurrentGold").GetComponent<TextMeshProUGUI>();
    }

    protected override UIState GetUIState()
    {
        return UIState.Score;
    }

    public void SetUI(int currentGold)
    {
        currentGoldText.text = currentGold.ToString();
    }
}