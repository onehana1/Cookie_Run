using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;
    public Button activateButton;

    void Start()
    {
        activateButton.onClick.AddListener(ActivateObject);
    }

    public void PauseButton()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    void ActivateObject()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}