using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarController : BaseController
{
    [Header("Scholar")]
    [SerializeField] private float skillCollTime; // ��ų ��Ÿ��
    [SerializeField] private float skillDurationTime = 5;
    [SerializeField] private float quizCooldownTime = 10f;  // ox




    [SerializeField] private GameObject skillEffect;
    [SerializeField] private Animator skillAnimator;
    [SerializeField] private bool isAutoSkillActive = true;

    [SerializeField] private StudySkill studySkill;


    private float skillCooldown = 5;
    private float quizCooldown = 0; // ox

    private bool isSkillActive = false;
 


    public event System.Action<float> OnSkillUsed;  // ��ų �̺�Ʈ
    public event System.Action<float> OnQuizUsed;  // ��ų �̺�Ʈ



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
        quizCooldown = 0;

        StartCoroutine(DelayedAutoSkillStart());
    }

    protected override void Update()
    {
        base.Update();
        if (!isSkillActive && skillCooldown > 0)
        {
            skillCooldown -= Time.deltaTime;
        }

        if (!studySkill.isQuizActive && quizCooldown > 0)
            quizCooldown -= Time.deltaTime;

        AddHandleAction();
    }

    protected void AddHandleAction()
    {
        if (isSkillActive || studySkill.isQuizActive) return;
        if(!baseState.isDead)  HandleSkill();
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

        if (Input.GetKeyDown(KeyCode.G))  // ox
        {
            if (quizCooldown > 0) return;
            StartCoroutine(OXQuizSkill());
        }
    }

    private IEnumerator AutoSkill()
    {
        Debug.Log("AutoSkill ����");
        while (isAutoSkillActive)
        {
            if (baseState.isDead) yield break;

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
        if (baseState.isDead) yield break;

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

    private IEnumerator OXQuizSkill()
    {
        if (baseState.isDead || studySkill.isQuizActive) yield break;

        baseState.StartInvincibility(invinvibleTime);
        StartBlinkEffect(invinvibleTime);

        Debug.Log("OX ���� ����!");
        quizCooldown = quizCooldownTime;

        UIManager.Instance.SetQuizMode(true);

        yield return StartCoroutine(studySkill.StartQuiz(studySkill.quizDuration));

        Debug.Log("�ڷ�ƾ ����");

        OnQuizUsed?.Invoke(quizCooldownTime);

        yield return new WaitForSeconds(studySkill.quizDuration);

        studySkill.isQuizActive = false;
        UIManager.Instance.SetQuizMode(false); // ui �������

        baseState.StartInvincibility(invinvibleTime);
        StartBlinkEffect(invinvibleTime);

        Debug.Log("OX ���� ����!");
    }



    public float GetSkillCooldownTime()
    {
        return skillCollTime;
    }

    public float GetQuizCooldownTime()
    {
        return quizCooldownTime;
    }

    public bool IsSkillOnCooldown()
    {
        return skillCooldown > 0;
    }
    public bool IsQuizOnCooldown()
    {
        return quizCooldown > 0;
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
