using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundUI : MonoBehaviour
{
    public GameObject SoundMenu;
    public Button activateButton;
    public bool isActive = false;

    public void OnButton()
    {
        SoundMananger.instance.PlayClickEffect();
        if (!isActive)
        {
            isActive = true;
            SoundMenu.SetActive(isActive);
        }
        else
        {
            isActive = false;
            SoundMenu.SetActive(isActive);
        }
    }
}
