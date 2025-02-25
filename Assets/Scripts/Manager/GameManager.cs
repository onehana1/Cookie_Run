using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int totalCoin;
    public List<int> Score;

    public BackGroundController backGroundController;

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
    }

    private void Start()
    {
        backGroundController = FindObjectOfType<BackGroundController>();
    }

    private void FixedUpdate()
    {
        UpdateDifficult();
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
