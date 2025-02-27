using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mode
{
    Infinite,
    Story
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //전 게임에서 획득한 코인, 스코어 기록
    public int preCoin;
    public List<int> Score;

    //보유 코인, 최고 기록
    public int totalCoin;
    public int bestScore;

    //게임내의 모드
    public Mode CurrentMode;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadBestScore();
        LoadTotalCoin();
    }

    //최고 점수 저장
    private void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
        PlayerPrefs.Save();
    }

    //최고 점수 불러오기
    private void LoadBestScore()
    {
        bestScore = GameManager.Instance.bestScore;
    }

    //보유 코인 수 저장
    private void SaveTotalCoin()
    {
        PlayerPrefs.SetInt("TotalCoin", totalCoin);
        PlayerPrefs.Save();
    }

    //보유 코인 수 불러오기
    private void LoadTotalCoin()
    {
        totalCoin = GameManager.Instance.totalCoin;
    }
}
