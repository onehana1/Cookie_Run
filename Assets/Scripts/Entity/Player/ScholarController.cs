using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarController : BaseController
{
    [SerializeField] private float skillDurationTime;
    [SerializeField] private float skillCollTime;


    protected override void Start()
    {
        base.Start();
        originalColliderSize = new Vector2(0.6f, 0.82f);
        slideColliderSize = new Vector2(originalColliderSize.x, 0.5f);
    }

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

    public override void Jump()
    {
        base.Jump();
    }

    public override void DoubleJump()
    {
        base.DoubleJump();
    }

    public override void StartSlide()
    {
        base.StartSlide();
    }
}
