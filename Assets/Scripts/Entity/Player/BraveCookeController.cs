using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BraveCookeController : BaseController
{

    protected override void Update()
    {
        base.Update();
    }

    private void HandleSkill()
    {
        if (Input.GetKeyDown(KeyCode.F))  // 예: 스킬 키가 F
        {
            StartCoroutine(BraveSkill());
        }
    }

    private IEnumerator BraveSkill()
    {
        Debug.Log("스킬 사용");
        float originalSpeed = baseState.moveSpeed;
        baseState.moveSpeed *= 2;  // 2배 속도로 대시

        yield return new WaitForSeconds(1.0f); // 1초 동안 지속

        baseState.moveSpeed = originalSpeed;  // 원래 속도로 복구
    }



}
