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
        if (Input.GetKeyDown(KeyCode.F))  // ��: ��ų Ű�� F
        {
            StartCoroutine(BraveSkill());
        }
    }

    private IEnumerator BraveSkill()
    {
        Debug.Log("��ų ���");
        float originalSpeed = baseState.moveSpeed;
        baseState.moveSpeed *= 2;  // 2�� �ӵ��� ���

        yield return new WaitForSeconds(1.0f); // 1�� ���� ����

        baseState.moveSpeed = originalSpeed;  // ���� �ӵ��� ����
    }



}
