using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;
    public Button activateButton;
    private bool isPaused = false;

    void Start()
    {
        activateButton.onClick.AddListener(TogglePause);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // P Ű �Է� ����
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused; // ���� ���� ����

        if (isPaused)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
}