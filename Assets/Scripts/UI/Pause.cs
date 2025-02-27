using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;
    public Button activateButton;
    public bool isActive = false;

    void Start()
    {
        if (activateButton != null)
        {
            activateButton.onClick.AddListener(ActivateObject);
        }
    }

    public void PauseButton()
    {
        SoundMananger.instance.PlayClickEffect();
        if (!isActive)
        {
            isActive = true;
            PauseMenu.SetActive(isActive);
            Time.timeScale = 0;
        }
        else
        {
            isActive = false;
            PauseMenu.SetActive(isActive);
            Time.timeScale = 1;
        }
    }

    void ActivateObject()
    {
        SoundMananger.instance.PlayClickEffect();
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}