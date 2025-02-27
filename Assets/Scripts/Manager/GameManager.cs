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

    //�� ���ӿ��� ȹ���� ����, ���ھ� ���
    public int preCoin;
    public List<int> Score;

    //���� ����, �ְ� ���
    public int totalCoin;
    public int bestScore;

    //���ӳ��� ���
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

    //�ְ� ���� ����
    private void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
        PlayerPrefs.Save();
    }

    //�ְ� ���� �ҷ�����
    private void LoadBestScore()
    {
        bestScore = GameManager.Instance.bestScore;
    }

    //���� ���� �� ����
    private void SaveTotalCoin()
    {
        PlayerPrefs.SetInt("TotalCoin", totalCoin);
        PlayerPrefs.Save();
    }

    //���� ���� �� �ҷ�����
    private void LoadTotalCoin()
    {
        totalCoin = GameManager.Instance.totalCoin;
    }
}
