using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private float quizING = 0;




    public event System.Action<float> OnSkillUsed;  // ��ų �̺�Ʈ
    public event System.Action<float> OnQuizUsed;  // ��ų �̺�Ʈ



    private void Awake()
    {
        skillDuration = skillDurationTime;
    }
    protected override void Start()
    {
        base.Start();
        originalColliderSize = new Vector2(0.4f, 0.7f);
        slideColliderSize = new Vector2(0.63f, 0.4f);
        slideColliderOffset = new Vector2(0, -0.03f);
        skillCooldown = skillCollTime;

        quizCooldown = quizCooldownTime;

        quizCooldown = 0;

        // StartCoroutine(DelayedAutoSkillStart());
        StartCoroutine(AutoQuizSkill());

    }

    protected override void Update()
    {
        base.Update();
        if (!isSkillActive && skillCooldown > 0)
        {
            skillCooldown -= Time.deltaTime;
        }

        if (!studySkill.isQuizActive && quizCooldown > 0)
        {
            quizCooldown -= Time.deltaTime;
        }

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

    private IEnumerator AutoSkill() // ���� ��ų
    {
        Debug.Log("AutoSkill ����");
        while (isAutoSkillActive)
        {
            if (baseState.isDead) yield break;

            if (skillCooldown <= 0)
            {
                Debug.Log("AutoSkill");
                quizING = 0;
                yield return StartCoroutine(Skill());
            }

            yield return new WaitForSeconds(skillCooldown > 0 ? skillCooldown : 0.1f);
        }
    }

    private IEnumerator AutoQuizSkill()
    {
        Debug.Log("AutoQuizSkill ����");
        while (isAutoSkillActive)
        {
            if (baseState.isDead) yield break;

            if (quizCooldown <= 0)
            {
                Debug.Log("AutoQuizSkill");
                yield return StartCoroutine(OXQuizSkill());

            }

            yield return new WaitForSeconds(quizCooldown > 0 ? quizCooldown : 0.1f);
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
        Debug.Log("OX ���� ����!");

        quizING += Time.deltaTime;
        baseState.isInvincible = true;
        animationHandler.SetSkill(true);
        isSkillActive = true;
        UIManager.Instance.SetQuizMode(true);

        quizCooldown = quizCooldownTime;        // ��Ÿ�� ����
        OnQuizUsed?.Invoke(quizCooldownTime);   // ��Ÿ�� ui�� �����ְ�

        
        yield return StartCoroutine(studySkill.StartQuiz(studySkill.quizDuration));
        animationHandler.SetSkill(false);
        baseState.isInvincible = false;
        isSkillActive = false;

        baseState.StartInvincibility(invinvibleTime);
        StartBlinkEffect(invinvibleTime);

        studySkill.isQuizActive = false;
        UIManager.Instance.SetQuizMode(false); // ui �������

        Debug.Log("���� �����ڰ�");

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
