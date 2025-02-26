using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager Instance;

    public BaseState playerState;
    public BackGroundController backGroundController;

    //�÷��� ����
    public int score;

    //ȹ�� ����
    public int coin;

    //�÷��� �ð�
    public float time;

    //�÷��̾��� ü��
    public float maxHp;
    public float hp;

    //�÷��� Ÿ��
    public float playTime = 0;

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
        playerState.OnTakeDamage += UpdateHp;
        playerState.OnDie += GameOver;
    }

    private void Start()
    {
        backGroundController = FindObjectOfType<BackGroundController>();
    }

    private void Update()
    {
        time = Time.deltaTime;
    }

    private void FixedUpdate()
    {
        UpdateDifficult();
    }
    //���� �����ֱ�
    public void AddScore(int scoreValue)
    {
        score += scoreValue;
    }
    //���� �����ֱ�
    public void AddCoin(int CoinValue)
    {
        coin += CoinValue;
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

    private void UpdateDifficult()
    {
        playTime += Time.unscaledDeltaTime;
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
}
