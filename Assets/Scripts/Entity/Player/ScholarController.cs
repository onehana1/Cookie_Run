using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarController : BaseController
{
    [Header("Scholar")]
    [SerializeField] private float skillCollTime; // ��ų ��Ÿ��
    [SerializeField] private float skillDurationTime = 5;

    [SerializeField] private GameObject skillEffect;
    [SerializeField] private Animator skillAnimator;
    [SerializeField] private bool isAutoSkillActive = true;

    private float skillCooldown = 5;
    private bool isSkillActive = false;

    public event System.Action<float> OnSkillUsed;  // ��ų �̺�Ʈ
    private void Awake()
    {
        skillDuration = skillDurationTime;
    }
    protected override void Start()
    {
        base.Start();
        originalColliderSize = new Vector2(0.6f, 0.82f);
        slideColliderSize = new Vector2(originalColliderSize.x, 0.5f);
        skillCooldown = skillCollTime;

        StartCoroutine(DelayedAutoSkillStart());
    }

    protected override void Update()
    {
        base.Update();
        if (!isSkillActive && skillCooldown > 0)
        {
            skillCooldown -= Time.deltaTime;
        }

        HandleSkill();
    }
    private IEnumerator DelayedAutoSkillStart()
    {
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(AutoSkill());
    }
    private void HandleSkill()
    {
        if (Input.GetKeyDown(KeyCode.F))  // ��: ��ų Ű�� F
        {
            if (skillCooldown > 0) return;

            StartCoroutine(Skill());
        }


    }
    public void UseSkill()
    {
        if (skillCooldown <= 0)
        {
            StartCoroutine(Skill());
        }
    }

    private IEnumerator AutoSkill()
    {
        Debug.Log("AutoSkill ����");
        while (isAutoSkillActive)
        {
            if (skillCooldown <= 0)
            {
                Debug.Log("AutoSkill");
                yield return StartCoroutine(Skill());
            }

            yield return new WaitForSeconds(skillCooldown > 0 ? skillCooldown : 0.1f);
        }
    }

    private IEnumerator Skill()
    {
        Debug.Log("��ų ���");
        baseState.isInvincible = true;
        animationHandler.SetSkill(true);
        skillEffect.SetActive(true);
        isSkillActive = true;

        float animationLength = skillAnimator.GetCurrentAnimatorStateInfo(0).length;
        skillAnimator.speed = animationLength / skillDuration;
        skillCooldown = skillCollTime;

        OnSkillUsed?.Invoke(skillCollTime); // ��ų ��� �̺�Ʈ �߻� -> ��Ÿ�� ui�� �������ٰ���

        yield return new WaitForSeconds(skillDuration); // ��ų ���� �ð�

        animationHandler.SetSkill(false);
        baseState.isInvincible = false;
        skillEffect.SetActive(false);
        isSkillActive = false;

        skillAnimator.speed = 1.0f;

        baseState.StartInvincibility(invinvibleTime);
        StartBlinkEffect(invinvibleTime);

    }

    public float GetSkillCooldownTime()
    {
        return skillCollTime;
    }
    public bool IsSkillOnCooldown()
    {
        return skillCooldown > 0;
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
