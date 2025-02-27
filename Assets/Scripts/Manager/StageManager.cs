using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ModeSelete(Mode mode)
    {
        GameManager.Instance.CurrentMode = mode;
        ChangeScene.ChangeGameScene();
    }
}
