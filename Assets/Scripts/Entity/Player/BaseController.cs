using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    // 애니메이션 연결 
    // 이동 => 이 없네..
    // 점프/더블점프
    // 착지
    // 슬라이드

    protected Rigidbody2D rb;
    protected AnimationHandler animationHandler;
    protected SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject jumpEffect;

    public BaseState baseState;

    [Header("Time")]
    [SerializeField] float hitTime = 0.5f;
    public float invinvibleTime = 3.0f;
    [SerializeField] float blinkIntervalTime = 0.5f;
    [SerializeField] float rescueTime = 2.0f;
    [SerializeField] float rescueLerpTime = 0.5f;
    public float skillDuration = 5.0f;



    [Header("Test")]
    [SerializeField] float groundY = -1.5f;
    [SerializeField] float returnGroundY = 2.0f;
    [SerializeField] float itemTime = 3.0f;
    [SerializeField] float damage = 10.0f;




    [Header("Collider Size")]
    private BoxCollider2D boxCollider;
    public Vector2 originalColliderSize = new Vector2(0.3f, 0.6f);
    public Vector2 originalColliderOffset = new Vector2(0.0f, -0.07f);

    public Vector2 slideColliderSize = new Vector2(0.5f, 0.4f);
    public Vector2 slideColliderOffset = new Vector2(0.0f, -0.01f);


    [Header("Raycast")]
    public float rayDistance = 1f;
    public LayerMask groundLayer;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        baseState = GetComponent<BaseState>();

        slideColliderSize = new Vector2(originalColliderSize.x, originalColliderSize.y * 0.5f);

        if (jumpEffect != null)
        {
            jumpEffect.SetActive(false); 
        }

        baseState.OnTakeDamage += HandleTakeDamage;
        baseState.OnDie += Die;
    }


    protected virtual void Update()
    {
        if (baseState.isDead) return;
        HandleAction();

        //if (isInvincible)   // 무적시간동안 안떨어지게..
        //{
        //    transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        //}
        if (transform.position.y < groundY && baseState.isLive)
        {
            if (!baseState.isRescue)
            {
                Debug.Log("구해줘요");
                StartRescue();
            }
        }
    }

    private void FixedUpdate()
    {
        OnGround();
        transform.position = new Vector3(-4f, transform.position.y, 0);//플레이어 고정
    }

    protected virtual void HandleAction()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!baseState.isJump)//baseState.isGrounded || 
            {
                Jump(); // 첫 번째 점프
            }
            else if (!baseState.isDoubleJump)
            {
                DoubleJump(); // 공중에서 한 번만 더 점프 가능
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && baseState.isGrounded)
        {
            StartSlide();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            EndSlide();
        }

        if (!baseState.isGrounded && rb.velocity.y < 0)
        {
            animationHandler.SetFalling(true);
            baseState.isFall = true;
        }

        // ========= 테스트 입니다.=========

        if (Input.GetKeyDown(KeyCode.H) && !baseState.isHit)    // 데미지 테스트
        {
            baseState.TakeDamage(damage);
        }

        if (Input.GetKeyDown(KeyCode.E))    // 빨리 달리기 테스트
        {
            SetRunningFast(itemTime);
        }

        if (Input.GetKeyDown(KeyCode.D))    // 죽음 테스트
        {
            baseState.Die();
            Debug.Log("죽니?");
        }

        if (Input.GetKeyDown(KeyCode.I))    // 무적 테스트
        {
            baseState.StartInvincibility(invinvibleTime);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SetBigger(itemTime);
        }
    }


    protected virtual void Move()
    {
        // 캐릭터는 무조건 앞으로가는 모션"만" 함 

    }

    public virtual void Jump()
    {
        if (baseState.isSliding)
        {
            EndSlide();
        }

        if (baseState.isGrounded || !baseState.isJump)
        {
            SoundMananger.instance.PlayJumpEffect();
            baseState.isJump = true;
            rb.velocity = Vector2.zero;
            rb.velocity = new Vector2(0f, baseState.jumpForce);
            baseState.isGrounded = false;
            animationHandler.SetJumping();
        }
    }

    public virtual void DoubleJump()
    {        
        if (baseState.isDoubleJump || !baseState.isJump) return;
        SoundMananger.instance.PlayJumpEffect();
        animationHandler.SetFalling(false);
        animationHandler.SetDoubleJump();

        rb.velocity = new Vector2(0f, baseState.jumpForce);
        baseState.isDoubleJump = true;
    }

    public virtual void StartSlide()
    {
        if (!baseState.isGrounded || baseState.isSliding) return; // 공중에서 슬라이드 x

        baseState.isSliding = true;
        animationHandler.SetSlide(true);

        // 콜라이더 크기 변경
        boxCollider.size = slideColliderSize;
        boxCollider.offset = slideColliderOffset;
        transform.position -= new Vector3(0, originalColliderSize.y - slideColliderSize.y - 0.1f, 0);
    }

    public void EndSlide()
    {
        if (!baseState.isGrounded || !baseState.isSliding) return;

        baseState.isSliding = false;
        animationHandler.SetSlide(false);

        // 원래 크기로 복구
        boxCollider.size = originalColliderSize;
        transform.position += new Vector3(0, originalColliderSize.y - slideColliderSize.y - 0.1f, 0);
    }

    private Coroutine runningFastCoroutine;

    private void SetRunningFast(float itemTime)
    {

        if (baseState.isFastRunning)
        {
            if (runningFastCoroutine != null) // 이미 켜져 있으면 끄고 다시 시작
            {
                StopCoroutine(runningFastCoroutine);
            }
            runningFastCoroutine = StartCoroutine(ResetRunningState(itemTime));
            return;
        }

        baseState.isFastRunning = true;
        animationHandler.SetRunning(1.0f); // 빠르게 달리기 상태

        runningFastCoroutine = StartCoroutine(ResetRunningState(itemTime));
    }




    private IEnumerator ResetRunningState(float itemTime)
    {
        yield return new WaitForSeconds(itemTime); // 주어진 시간 동안 대기

        baseState.isFastRunning = false;
        animationHandler.SetRunning(0.0f); // 원래 속도로 복귀
    }

    private Coroutine biggerCoroutine;

    public void SetBigger(float itemTime)
    {

        if (baseState.isBigger)
        {
            if (biggerCoroutine != null) // 이미 켜져 있으면 끄고 다시 시작
            {
                StopCoroutine(biggerCoroutine);
            }
            biggerCoroutine = StartCoroutine(ResetBigger(itemTime));
            return;
        }
        baseState.isBigger = true;

        biggerCoroutine = StartCoroutine(GrowOverTime(itemTime));
    }

    private IEnumerator ResetBigger(float itemTime)
    {
        yield return new WaitForSeconds(itemTime); // 주어진 시간 동안 대기

        baseState.isBigger = false;
        transform.localScale /= 2.0f;   // 원래 크기로 복귀 시킴
    }

    private IEnumerator GrowOverTime(float itemTime)
    {
        float duration = 0.5f;  // 크기가 커지는 데 걸리는 시간
        float time = 0f;
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = startScale * 2.0f;  // 2배 크기로 목표 설정
        rayDistance = 1.5f;


        while (time < duration)
        {
            float t = time / duration;
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);

            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;

        yield return new WaitForSeconds(itemTime); // 유지 시간

        yield return StartCoroutine(ShrinkOverTime()); // 점진적으로 원래 크기로 복귀
    }

    private IEnumerator ShrinkOverTime()
    {
        float duration = 0.5f; // 작아지는 시간
        float time = 0f;
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = startScale / 2.0f; // 원래 크기로 복귀
        rayDistance = 1;


        while (time < duration)
        {
            float t = time / duration;
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);


            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;

        baseState.isBigger = false;
    }

    private void HandleTakeDamage(float maxhp, float currentHp)
    {
        Debug.Log($"체력 업데이트: {currentHp}");
        animationHandler.SetHit(true);
        baseState.StartInvincibility(invinvibleTime);
        StartCoroutine(BlinkEffect(invinvibleTime));
        StartCoroutine(ResetHitState());
    }


    private IEnumerator ResetHitState() // OnStateExit vs 코루틴 생각하다가 피격 애니메이션 1프레임 고정이라 코루틴 사용
    {
        yield return new WaitForSeconds(hitTime); // 피격 애니메이션 길이만큼 대기
        baseState.isHit = false;

        animationHandler.SetHit(false);
        animationHandler.SetRunning(1.0f);  // 다시 뛰는 애니메이션
    }

    private void Die()
    {
        if (baseState.isLive) return;
        baseState.TakeDamage(1000);
        boxCollider.size = slideColliderSize;
        animationHandler.SetHit(false);
        animationHandler.SetDie();
    }


    private void StartRescue()
    {
        Vector3 targetPos = new Vector3(transform.position.x, returnGroundY, transform.position.z);
        rb.gravityScale = 0.0f;
        baseState.isRescue = true;
        animationHandler.SetRescue(true);
        StartCoroutine(LerpToRescuePoint(transform.position, targetPos));
    }

    private IEnumerator LerpToRescuePoint(Vector3 startPos, Vector3 targetPos)
    {
        float duration = rescueLerpTime;
        float time = 0f;
        StartCoroutine(BlinkEffect(duration));
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
        rb.velocity = Vector2.zero;
        StartCoroutine(StayInAir(rescueTime));
    }

    private IEnumerator StayInAir(float time)
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(time);
        StartCoroutine(RescueToLand());
    }
    private IEnumerator RescueToLand()
    {
        if (baseState.isGrounded) yield return null;
        baseState.isRescue = false;
        animationHandler.SetRescue(false);
        rb.gravityScale = 1.0f;
        rb.velocity = Vector2.zero;
        Debug.Log("구해줬어요");

        baseState.StartInvincibility(invinvibleTime);
        yield return null;
    }

    public void StartBlinkEffect(float duration)
    {
        StartCoroutine(BlinkEffect(duration));
    }
    private IEnumerator BlinkEffect(float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f); // 투명도 조절
            yield return new WaitForSeconds(blinkIntervalTime);
            spriteRenderer.color = new Color(1, 1, 1, 1.0f);
            yield return new WaitForSeconds(blinkIntervalTime);
            time += blinkIntervalTime * 2;
        }
        spriteRenderer.color = new Color(1, 1, 1, 1); // 원래 색으로 복구
    }

    private Coroutine skillCoroutine;
    private void UseSkill(float skillDuration)
    {
        if (skillCoroutine != null)
        {
            StopCoroutine(skillCoroutine);
        }
        skillCoroutine = StartCoroutine(SkillAnimation(skillDuration));
    }

    private IEnumerator SkillAnimation(float duration)
    {
        animationHandler.SetSkill(true);
        yield return new WaitForSeconds(duration);
        animationHandler.SetSkill(false);
    }

    private void PlayJumpEffect()
    {
        if(jumpEffect != null)
        {
            jumpEffect.SetActive(true);
            StartCoroutine(DisableJumpEffect());
        }
    }
    private IEnumerator DisableJumpEffect()
    {
        yield return new WaitForSeconds(0.5f);
        if (jumpEffect != null)
        {
            jumpEffect.SetActive(false);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            SoundMananger.instance.PlayObstacleEffect();
            Debug.Log("충돌");
            baseState.TakeDamage(damage);

            if (!baseState.isLive)
                baseState.Die();
        }
    }



    protected virtual void OnGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, groundLayer);

        if (hit.collider != null)
        {
            if (!baseState.isGrounded) PlayJumpEffect();
            if (!baseState.isRand || baseState.isFall)
            {
                animationHandler.SetFalling(false);
                baseState.isJump = false;
                baseState.isFall = false;
                baseState.isDoubleJump = false;
                baseState.isGrounded = true;
                baseState.isRand = true;
                animationHandler.SetLanding();
                animationHandler.SetRescue(false);
                Debug.Log("닿았다");
            }
        }
        else
        {
            if (baseState.isRand)
            {
                baseState.isRand = false;
            }
            baseState.isGrounded = false;
        }

        Debug.DrawRay(transform.position, Vector2.down * rayDistance, Color.red);
    }
}
