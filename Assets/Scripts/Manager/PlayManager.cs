using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager Instance;

    public BackGroundController backGroundController;
    public GameManager gameManager;
    public FadeContrller fadeContrller;
    public BaseState playerState;

    //��ǥġ(���� Ȥ�� Ÿ��)
    private float goal;

    //?�레???�수
    public int score;

    //최고 ?�수
    public int bestScore;

    //?�득 코인
    public int coin;

    //?�레?�어??체력
    public float maxHp;
    public float hp;

    //?�레???�??
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

        // 체력 변???�벤??구독
        playerState.OnHpChanged += UpdateHp;
        playerState.OnDie += GameOver;

    }

    private void Start()
    {
        backGroundController = FindObjectOfType<BackGroundController>();
        gameManager = GameManager.Instance;
        playerState = GameObject.FindWithTag("Player").GetComponent<BaseState>();
    }

    private void Update()
    {
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
        UpdateDifficult();
    }

    //���� ���������� �����ֱ�
    public void AddScore(int scoreValue)
    {
        score += scoreValue;
    }

    //코인 지?�적?�로 ?�해주기
    public void AddCoin(int CoinValue)
    {
        coin += CoinValue;
    }

    public void AddTotalCoin()
    {
        totalCoin += coin;
        SaveTotalCoin();
    }

    //ü�� ����
    private void UpdateHp(float maxHp, float currentHp)
    {
        this.hp = currentHp;
    }

    //���� ������ ���ΰ� ������ ���ӸŴ��� �ν��Ͻ��� ��������
    public void GameOver()
    {
        gameManager.Score.Add(score);
        gameManager.totalCoin += coin;
        gameManager.bestScore = score > gameManager.bestScore ? score : gameManager.bestScore;
        Time.timeScale = 0f;

        ////���ھ ��ǥġ�� �޼�������
        //if (score >= goal)
        //{
        //    SceneManager.LoadScene("ResultScene");
        //}
        ////���ھ ��ǥġ �޼��� ���������� 
        //else
        //{
        //    SceneManager.LoadScene("ResultScene");
        //}

        ////�÷��� �ð��� ��ǥġ�� �޼�������
        //if (score >= time)
        //{
        //    SceneManager.LoadScene("ResultScene");
        //}
        ////�÷��� �ð��� ��ǥġ �޼��� ���������� 
        //else
        //{
        //    SceneManager.LoadScene("ResultScene");
        //}

        //�������ְ� �ϴ°� �ʿ�
    }
    //���̵� ����
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
            playTimeText.text = FormatTime(time);

        if (coinText != null)
            coinText.text = coin.ToString();

        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString("N0");

        if (bestScoreText != null)
            bestScoreText.text = gameManager.bestScore.ToString();

        if (totalCoinText != null)
            totalCoinText.text = gameManager.totalCoin.ToString();
    }

    private string FormatTime(float timeInSeconds)
    {
        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(timeInSeconds);
        return string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
    }
}
