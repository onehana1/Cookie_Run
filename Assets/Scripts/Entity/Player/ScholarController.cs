using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarController : BaseController
{
    [SerializeField] private float skillDurationTime;
    [SerializeField] private float skillCollTime;
    [SerializeField] private GameObject skillEffect;
    [SerializeField] private Animator skillAnimator;



    protected override void Start()
    {
        base.Start();
        originalColliderSize = new Vector2(0.6f, 0.82f);
        slideColliderSize = new Vector2(originalColliderSize.x, 0.5f);
    }

    protected override void Update()
    {
        base.Update();
        HandleSkill();
    }

    private void HandleSkill()
    {
        if (Input.GetKeyDown(KeyCode.F))  // 예: 스킬 키가 F
        {
            StartCoroutine(Skill());
        }
    }

    private IEnumerator Skill()
    {
        Debug.Log("스킬 사용");
        baseState.isInvincible = true;
        animationHandler.SetSkill(true);
        skillEffect.SetActive(true);

        float animationLength = skillAnimator.GetCurrentAnimatorStateInfo(0).length;
        skillAnimator.speed = animationLength / skillDuration;

        yield return new WaitForSeconds(skillDuration);

        animationHandler.SetSkill(false);
        baseState.isInvincible = false;
        skillEffect.SetActive(false);

        skillAnimator.speed = 1.0f;
        baseState.StartInvincibility(invinvibleTime);
        StartBlinkEffect(invinvibleTime);

    }

}
