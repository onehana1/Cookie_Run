using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BraveCookeController : BaseController
{
    [SerializeField] private float skillCollTime;



    protected override void Update()
    {
        base.Update();
        HandleSkill();
    }

    private void HandleSkill()
    {
        if (Input.GetKeyDown(KeyCode.F))  // ��: ��ų Ű�� F
        {
            StartCoroutine(BraveSkill());
        }
    }

    private IEnumerator BraveSkill()
    {
        Debug.Log("��ų ���");
        baseState.isInvincible = true;
        animationHandler.SetSkill(true);

        float duration = skillDuration;

        while (duration > 0)
        {
            duration -= Time.deltaTime;
            yield return null;
        }

        animationHandler.SetSkill(false);
        baseState.isInvincible = false;

    }



}
