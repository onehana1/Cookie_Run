using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayManager : MonoBehaviour
{
    public static PlayManager Instance;

    public BaseState playerState;
    public BackGroundController backGroundController;

    //�÷��� ����
    public int score;

    //�ְ� ����
    public int bestScore;

    //ȹ�� ����
    public int coin;

    //���� ����
    public int totalCoin;

    //�÷��̾��� ü��
    public float maxHp;
    public float hp;

    //�÷��� Ÿ��
    public float playTime = 0;


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
        hp = playerState.hp;
        maxHp = playerState.maxHp;

        // ü�� ��ȭ �̺�Ʈ ����
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
        UpdateDifficult();
    }

    //���� �����ֱ�
    public void AddScore(int scoreValue)
    {
        score += scoreValue;
        if (score > bestScore)
        {
            bestScore = score;
            SaveBestScore();
        }
    }

    //���� �����ֱ�
    public void AddCoin(int CoinValue)
    {
        coin += CoinValue;
    }

    public void AddTotalCoin()
    {
        totalCoin += coin;
        SaveTotalCoin();
    }

    private void UpdateHp(float maxHp, float currentHp)
    {
        this.hp = currentHp;
    }


    //���� ������ ���ΰ� ������ ���ӸŴ��� �ν��Ͻ��� ��������
    public void GameOver()
    {
        GameManager.Instance.Score.Add(score);
        GameManager.Instance.totalCoin += coin;
    }

    //���̵� ����
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

    //�ְ� ���� ����
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

    //�ְ� ���� �ҷ�����
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
