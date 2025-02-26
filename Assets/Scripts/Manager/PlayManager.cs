using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayManager : MonoBehaviour
{
    public static PlayManager Instance;

    public BaseState playerState;
    public BackGroundController backGroundController;
    public GameManager gameManager;
    public FadeContrller fadeContrller;

    //플레이 점수
    public int score;

    //최고 점수
    public int bestScore;

    //획득 코인
    public int coin;

    //보유 코인
    public int totalCoin;

    //플레이어의 체력
    public float maxHp;
    public float hp;

    //플레이 타임
    public float time = 0;
    public float endTime = 180;

    public float playTime = 0;

    public bool isEnd = false;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI totalCoinText;
    public TextMeshProUGUI playTimeText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        playerState = GameObject.FindWithTag("Player").GetComponent<BaseState>();
        fadeContrller = GetComponent<FadeContrller>();

        hp = playerState.hp;
        maxHp = playerState.maxHp;

        // 체력 변화 이벤트 구독
        playerState.OnHpChanged += UpdateHp;
        playerState.OnDie += GameOver;

        LoadBestScore();
        LoadTotalCoin();
    }

    private void Start()
    {
        backGroundController = FindObjectOfType<BackGroundController>();
    }

    private void Update()
    {
        playTime += Time.deltaTime;
        UpdateUI();
    }

    private void FixedUpdate()
    {
        playTime += Time.unscaledDeltaTime;
        time += Time.unscaledDeltaTime;
        if (time >= endTime && !isEnd) 
        { 
            isEnd = true;
            Time.timeScale = 1.0f;
            backGroundController.backGroundImageWidth = fadeContrller.backGroundSprite.bounds.size.x * 2.5f;
            fadeContrller.OnFadaOutandIn();
        }

        UpdateDifficult();
    }

    //점수 지속적으로 더해주기
    public void AddScore(int scoreValue)
    {
        score += scoreValue;
        if (score > bestScore)
        {
            bestScore = score;
            SaveBestScore();
        }
    }

    //코인 지속적으로 더해주기
    public void AddCoin(int CoinValue)
    {
        coin += CoinValue;
    }

    public void AddTotalCoin()
    {
        totalCoin += coin;
        SaveTotalCoin();
    }

    //체력 갱신
    private void UpdateHp(float maxHp, float currentHp)
    {
        this.hp = currentHp;
    }


    //게임 오버시 코인과 점수를 게임매니저 인스턴스에 저장해줌
    public void GameOver()
    {
        GameManager.Instance.Score.Add(score);
        GameManager.Instance.totalCoin += coin;
    }

    //난이도 증가
    private void UpdateDifficult()
    {
        //playTime += Time.unscaledDeltaTime;
        if (playTime > 30)
        {
            playTime = 0;
            backGroundController.currentTimeScale += 0.2f;
            if (backGroundController.currentTimeScale > 3)
            {
                backGroundController.currentTimeScale = 3;
            }
        }
    }

    private void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
        PlayerPrefs.Save();
    }

    private void SaveTotalCoin()
    {
        PlayerPrefs.SetInt("TotalCoin", totalCoin);
        PlayerPrefs.Save();
    }

    private void LoadTotalCoin()
    {
        totalCoin = PlayerPrefs.GetInt("TotalCoin", 0);
    }

    private void LoadBestScore()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    private void UpdateUI()
    {
        if (playTimeText != null)
            playTimeText.text = "Time: " + (int)playTime;

        if (coinText != null)
            coinText.text = "Coin: " + coin;

        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (bestScoreText != null)
            bestScoreText.text = "Best Score: " + bestScore;

        if (totalCoinText != null)
            totalCoinText.text = "Total Coin: " + totalCoin;
    }
}
