using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScholarController : BaseController
{
    [Header("Scholar")]
    [SerializeField] private float skillCollTime; // 스킬 쿨타임
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




    public event System.Action<float> OnSkillUsed;  // 스킬 이벤트
    public event System.Action<float> OnQuizUsed;  // 스킬 이벤트



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
       
        if (Input.GetKeyDown(KeyCode.F))  // 예: 스킬 키가 F
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

    private IEnumerator AutoSkill() // 기존 스킬
    {
        Debug.Log("AutoSkill 시작");
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
        Debug.Log("AutoQuizSkill 시작");
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

        Debug.Log("스킬 사용");
        baseState.isInvincible = true;
        animationHandler.SetSkill(true);
        skillEffect.SetActive(true);
        isSkillActive = true;

        float animationLength = skillAnimator.GetCurrentAnimatorStateInfo(0).length;
        skillAnimator.speed = animationLength / skillDuration;
        skillCooldown = skillCollTime;

        OnSkillUsed?.Invoke(skillCollTime); // 스킬 사용 이벤트 발생 -> 쿨타임 ui에 전달해줄거임

        yield return new WaitForSeconds(skillDuration); // 스킬 지속 시간

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
        Debug.Log("OX 퀴즈 시작!");

        quizING += Time.deltaTime;
        baseState.isInvincible = true;
        animationHandler.SetSkill(true);
        isSkillActive = true;
        UIManager.Instance.SetQuizMode(true);

        quizCooldown = quizCooldownTime;        // 쿨타임 설정
        OnQuizUsed?.Invoke(quizCooldownTime);   // 쿨타임 ui에 보내주고

        
        yield return StartCoroutine(studySkill.StartQuiz(studySkill.quizDuration));
        animationHandler.SetSkill(false);
        baseState.isInvincible = false;
        isSkillActive = false;

        baseState.StartInvincibility(invinvibleTime);
        StartBlinkEffect(invinvibleTime);

        studySkill.isQuizActive = false;
        UIManager.Instance.SetQuizMode(false); // ui 원래대로

        Debug.Log("무적 가보자고");

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
