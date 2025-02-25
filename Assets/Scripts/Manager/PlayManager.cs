using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager Instance;

    //�÷��� ����
    public int score;
    //ȹ�� ����
    public int coin;

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
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void AddCoin(int CoinValue)
    {
        coin += CoinValue;
    }
}
