using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager Instance;

    public BaseState playerState;

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
    }

    private void Update()
    {
        time = Time.deltaTime;
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

    //���� ������ ���ΰ� ������ ���ӸŴ��� �ν��Ͻ��� ��������
    public void gameOver()
    {
        GameManager.Instance.Score.Add(score);
        GameManager.Instance.totalCoin += coin;
    }
}
