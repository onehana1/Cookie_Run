using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    public BaseState playerState;
    public Image hpBar;

    void Start()
    {
        playerState.OnTakeDamage += UpdateHpBar;
    }

    private void UpdateHpBar(float MaxHp, float currentHp)
    {
        if (hpBar != null) 
        {
            hpBar.fillAmount = currentHp / MaxHp;
        }
    }
}