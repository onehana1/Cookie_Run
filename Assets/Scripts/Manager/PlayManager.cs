using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public enum Goal 
{
    Score,
    Time
}


public class PlayManager : MonoBehaviour
{
    public static PlayManager Instance;

    public BackGroundController backGroundController;
    public BaseState playerState;
    private GameManager gameManager;

    //목표치(점수 혹은 타임)
    private float goal;

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

        hp = playerState.hp;
        maxHp = playerState.maxHp;

        // 체력 변화 이벤트 구독
        playerState.OnHpChanged += UpdateHp;
        playerState.OnDie += GameOver;

        ////목표치 설정
        //if (Random.Range(0, 2) <1)
        //{
            
        //}
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
        Time.timeScale = 0f;

        ////스코어가 목표치를 달성했을때
        //if (score >= goal)
        //{
        //    SceneManager.LoadScene("ResultScene");
        //}
        ////스코어가 목표치 달성을 실패했을때 
        //else
        //{
        //    SceneManager.LoadScene("ResultScene");
        //}

        ////플레이 시간이 목표치를 달성했을때
        //if (score >= time)
        //{
        //    SceneManager.LoadScene("ResultScene");
        //}
        ////플레이 시간이 목표치 달성을 실패했을때 
        //else
        //{
        //    SceneManager.LoadScene("ResultScene");
        //}
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
            playTimeText.text = playTime.ToString("N2");

        if (coinText != null)
            coinText.text = coin.ToString();

        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString("N2");

        if (bestScoreText != null)
            bestScoreText.text = gameManager.bestScore.ToString();

        if (totalCoinText != null)
            totalCoinText.text = gameManager.totalCoin.ToString();
    }
}
