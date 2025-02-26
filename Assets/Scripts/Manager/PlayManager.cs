using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager Instance;

    public BackGroundController backGroundController;
    public BaseState playerState;
    private GameManager gameManager;

    //��ǥġ(���� Ȥ�� Ÿ��)
    private float goal;

    //�÷��� ����
    public int score;

    //ȹ�� ����
    public int coin;

    //�÷��̾��� ü��
    public float maxHp;
    public float hp;

    //�÷��� Ÿ��
    public float time = 0;

    //���̵� ���� Ÿ��
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

        // ü�� ��ȭ �̺�Ʈ ����
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
        UpdateDifficult();
    }

    //���� ���������� �����ֱ�
    public void AddScore(int scoreValue)
    {
        score += scoreValue;
    }

    //���� ���������� �����ֱ�
    public void AddCoin(int CoinValue)
    {
        coin += CoinValue;
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
            playTimeText.text = playTime.ToString("N0");

        if (coinText != null)
            coinText.text = coin.ToString();

        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString("N0");

        if (bestScoreText != null)
            bestScoreText.text = gameManager.bestScore.ToString();

        if (totalCoinText != null)
            totalCoinText.text = gameManager.totalCoin.ToString();
    }
}
