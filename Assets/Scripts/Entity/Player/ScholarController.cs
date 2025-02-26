using System.Collections;
using System.Collections.Generic;
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
 


    public event System.Action<float> OnSkillUsed;  // 스킬 이벤트
    public event System.Action<float> OnQuizUsed;  // 스킬 이벤트



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

    private IEnumerator AutoSkill()
    {
        Debug.Log("AutoSkill 시작");
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

        baseState.StartInvincibility(invinvibleTime);
        StartBlinkEffect(invinvibleTime);

        Debug.Log("OX 퀴즈 시작!");
        quizCooldown = quizCooldownTime;

        UIManager.Instance.SetQuizMode(true);

        yield return StartCoroutine(studySkill.StartQuiz(studySkill.quizDuration));

        Debug.Log("코루틴 이후");

        OnQuizUsed?.Invoke(quizCooldownTime);

        yield return new WaitForSeconds(studySkill.quizDuration);

        studySkill.isQuizActive = false;
        UIManager.Instance.SetQuizMode(false); // ui 원래대로

        baseState.StartInvincibility(invinvibleTime);
        StartBlinkEffect(invinvibleTime);

        Debug.Log("OX 퀴즈 종료!");
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
