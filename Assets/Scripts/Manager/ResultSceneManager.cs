using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultSceneManager : MonoBehaviour
{
    GameManager gameManager;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI CoinText;

    private void Start()
    {
        gameManager = GameManager.Instance;
        UpdateBestScoreUI();
    }

    void UpdateBestScoreUI()
    {
        int bestScore = gameManager.bestScore;
        bestScoreText.text = bestScore.ToString("N0");

        int lastScore = gameManager.Score[gameManager.Score.Count - 1];
        ScoreText.text = lastScore.ToString("N0");

        int coin = gameManager.preCoin ;
        CoinText.text = coin.ToString("N0");
    }
}