using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;


public class PlayManager : MonoBehaviour
{
    public static PlayManager Instance;

    public BackGroundController backGroundController;
    public FadeController fadeContrller;
    public GameManager gameManager;
    public BaseState playerState;

    //목표치(점수 혹은 타임)
    private float goalTime;
    private float goalScore;
    
    //게임 경과시간, 난이도 조절용 타임
    private float time = 0;
    private float playTime = 0;

    //플레이어의 체력
    public float maxHp;
    public float hp;

    //플레이 점수
    public int score;

    //획득 코인
    public int coin;

    //플레이 타임
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
        fadeContrller = GetComponent<FadeController>();

        hp = playerState.hp;
        maxHp = playerState.maxHp;

        // 체력 변화 이벤트 구독
        playerState.OnHpChanged += UpdateHp;
        playerState.OnDie += GameOver;

        time = 0;
        score = 0;
    }

    private void Start()
    {
        backGroundController = FindObjectOfType<BackGroundController>();
        gameManager = GameManager.Instance;
        playerState = GameObject.FindWithTag("Player").GetComponent<BaseState>();
        

        //스토리모드 일 때
        if (gameManager.CurrentMode == Mode.Story)
        {
            //목표 시간 설정
            goalTime = 180;
            goalScore = 15000;
        }
        gameManager.preCoin = 0;
    }

    private void Update()
    {
        UpdateUI();

        if (isEnd)
        {
            backGroundController.backGroundImageWidth = fadeContrller.backGroundSprite.bounds.size.x * 2.5f;
            fadeContrller.OnFadaOutandIn();
        }
    }

    private void FixedUpdate()
    {
        playTime += Time.unscaledDeltaTime;
        time += Time.unscaledDeltaTime;

        //스토리모드의 엔딩조건
        if (gameManager.CurrentMode == Mode.Story)
        {
            if (time >= goalTime && !isEnd)
            {
                isEnd = true;
                GameOver();
            }
        }
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
        Time.timeScale = 1.0f;

        gameManager.Score.Add(score);
        gameManager.preCoin = coin;
        gameManager.totalCoin += coin;

        //스토리모드 
        gameManager.bestScore = score > gameManager.bestScore ? score : gameManager.bestScore;
        if (gameManager.CurrentMode == Mode.Story)
        {
            //목표시간에 달성하지 않았을 때
            if (time < goalTime)
            {
                StartCoroutine(GivemeDelay(5f, 0));
            }

            //스코어가 목표치 달성을 실패했을때 
            else if (score < goalScore)
            {
                StartCoroutine(GivemeDelay(5f, 0));
            }

            //스코어가 목표치를 달성했을 때
            else
            {
                StartCoroutine(GivemeDelay(5f, 1));
            }
        }

        else
        {
            StartCoroutine(GivemeDelay(5f));
        }
    }

    //스토리모드 컷신 로드하는 코루틴
    IEnumerator GivemeDelay(float second, int state)
    {
        backGroundController.backGroundImageWidth = fadeContrller.backGroundSprite.bounds.size.x * 2.5f;
        fadeContrller.OnFadaOutandIn();
        yield return new WaitForSeconds(second);
        if (state == 0)
        {
            ChangeScene.ChangeResultBadScene();
        }
        else
        {
            ChangeScene.ChangeResultGoodScene();
        }
    }

    //무한모드 결과창 로드하는 코루틴
    IEnumerator GivemeDelay(float second)
    {
        backGroundController.backGroundImageWidth = fadeContrller.backGroundSprite.bounds.size.x * 2.5f;
        fadeContrller.OnFadaOutandIn();
        yield return new WaitForSeconds(second);
        ChangeScene.ChangeResultScene();
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

    private void SetTimeToStringPretty()
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        playTimeText.text = $"{minutes:D2}:{seconds:D2}";
    }

    private void UpdateUI()
    {
        if (playTimeText != null)
            SetTimeToStringPretty();

        if (coinText != null)
            coinText.text = coin.ToString();

        if (scoreText != null)
            scoreText.text = score.ToString("N0");

        if (bestScoreText != null)
            bestScoreText.text = gameManager.bestScore.ToString();

        if (totalCoinText != null)
            totalCoinText.text = gameManager.totalCoin.ToString();
    }
}
