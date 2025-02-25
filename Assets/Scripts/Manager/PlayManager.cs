using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager Instance;

    public BaseState playerState;

    //플레이 점수
    public int score;

    //획득 코인
    public int coin;

    //플레이 시간
    public float time;

    //플레이어의 체력
    public float maxHp;
    public float hp;

    //플레이 타임
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

    //점수 더해주기
    public void AddScore(int scoreValue)
    {
        score += scoreValue;
    }
    //코인 더해주기
    public void AddCoin(int CoinValue)
    {
        coin += CoinValue;
    }

    //게임 오버시 코인과 점수를 게임매니저 인스턴스에 저장해줌
    public void gameOver()
    {
        GameManager.Instance.Score.Add(score);
        GameManager.Instance.totalCoin += coin;
    }
}
