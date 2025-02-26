using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayManager : MonoBehaviour
{
    public static PlayManager Instance;

    public BackGroundController backGroundController;
    public BaseState playerState;
    private GameManager gameManager;


    //플레이 점수
    public int score;

    //획득 코인
    public int coin;

    //플레이어의 체력
    public float maxHp;
    public float hp;

    //플레이 타임
    public float time = 0;

    //난이도 조절 타임
    private float playTime = 0;

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

        //필드 연결작업
        gameManager = GameManager.Instance;
        playerState = GameObject.FindWithTag("Player").GetComponent<BaseState>();

        hp = playerState.hp;
        maxHp = playerState.maxHp;

        // 체력 변화 이벤트 구독
        playerState.OnHpChanged += UpdateHp;
        playerState.OnDie += GameOver;
    }

    private void Start()
    {
        backGroundController = FindObjectOfType<BackGroundController>();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void FixedUpdate()
    {
        playTime += Time.unscaledDeltaTime;
        time += Time.unscaledDeltaTime;
        UpdateDifficult();
    }

    //점수 지속적으로 더해주기
    public void AddScore(int scoreValue)
    {
        score += scoreValue;
    }

    //코인 지속적으로 더해주기
    public void AddCoin(int CoinValue)
    {
        coin += CoinValue;
    }

    //체력 갱신
    private void UpdateHp(float maxHp, float currentHp)
    {
        this.hp = currentHp;
    }


    //게임 오버시 코인과 점수를 게임매니저 인스턴스에 저장해줌
    public void GameOver()
    {
        gameManager.Score.Add(score);
        gameManager.totalCoin += coin;

        gameManager.bestScore = score > gameManager.bestScore ? score : gameManager.bestScore;
    }

    //난이도 증가
    private void UpdateDifficult()
    {
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

    private void UpdateUI()
    {
        if (playTimeText != null)
            playTimeText.text = "Time: " + (int)playTime;

        if (coinText != null)
            coinText.text = "Coin: " + coin;

        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (bestScoreText != null)
            bestScoreText.text = "Best Score: " + gameManager.bestScore;

        if (totalCoinText != null)
            totalCoinText.text = "Total Coin: " + gameManager.totalCoin;
    }
}
