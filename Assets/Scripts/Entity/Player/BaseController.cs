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

    private BaseState baseState;

    [Header("Time")]
    [SerializeField] float hitTime = 0.5f;
    [SerializeField] float invinvibleTime = 3.0f;
    [SerializeField] float blinkIntervalTime = 0.5f;    
    [SerializeField] float rescueTime = 2.0f;
    [SerializeField] float rescueLerpTime = 0.5f;


    [Header("Test")]
    [SerializeField] float groundY = -1.5f;
    [SerializeField] float returnGroundY = 2.0f;



    protected bool isFastRunning = false;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        baseState = GetComponent<BaseState>();
    }

    protected virtual void Update()
    {
        if (baseState.isHit) return;
        HandleAction();

        //if (isInvincible)   // 무적시간동안 안떨어지게..
        //{
        //    transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        //}

        if ( transform.position.y < groundY && baseState.isLive)
        {
            if (!baseState.isRescue)
            {
                Debug.Log("구해줘요");
                StartRescue();
            }
        }


    }

    protected void HandleAction()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (baseState.isGrounded)
            {
                Jump(); // 첫 번째 점프
            }
            else if (baseState.isDoubleJump)
            {
                DoubleJump(); // 공중에서 한 번만 더 점프 가능
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && baseState.isGrounded)
        {
            StartSlide();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            EndSlide();
        }

        if (!baseState.isGrounded && rb.velocity.y <= 0)
        {
            animationHandler.SetFalling(true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            isFastRunning = !isFastRunning; // 상태 토글
            float newSpeed = isFastRunning ? 0.0f : 1.0f;
            animationHandler.SetRunning(newSpeed);
        }

        if (Input.GetKeyDown(KeyCode.H) && !baseState.isHit)
        {
            TakeHit();
        }

        if (Input.GetKeyDown(KeyCode.D))    // 테스트
        {
            Die();
        }

        if (Input.GetKeyDown(KeyCode.I))    // 테스트
        {
            baseState.StartInvincibility(invinvibleTime);
        }
    }


    protected virtual void Move()
    {
        // 캐릭터는 무조건 앞으로가는 모션"만" 함 

    }

    protected virtual void Jump()
    {
        if (baseState.isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.velocity = new Vector2(0f, baseState.jumpForce);
            baseState.isGrounded = false;
            baseState.isDoubleJump = true;
            animationHandler.SetJumping();
        }
    }

    protected virtual void DoubleJump()
    {
        if (!baseState.isDoubleJump) return;

        rb.velocity = new Vector2(0f, baseState.jumpForce);
        baseState.isDoubleJump = false; 
        animationHandler.SetDoubleJump();
    }

    private void StartSlide()
    {
        if (!baseState.isGrounded) return; // 공중에서 슬라이드 x

        baseState.isSliding = true;
        animationHandler.SetSlide(true);
    }

    private void EndSlide()
    {
        baseState.isSliding = false;
        animationHandler.SetSlide(false);
    }








    private void TakeHit()
    {
        if (baseState.isHit || baseState.isInvincible) return;

        baseState.isHit = true;
        animationHandler.SetHit(true);

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
        baseState.isLive = false;

        if(!baseState.isLive)
            animationHandler.SetDie();

        else
        {
            animationHandler.ResetDie();
            animationHandler.SetRunning(1.0f);
        }
    }

    private void StartRescue()
    {
        Vector3 targetPos = new Vector3(transform.position.x, returnGroundY, transform.position.z);
        rb.gravityScale = 0.0f;
        baseState.isRescue = true;
        animationHandler.SetRescue(true);
        StartCoroutine(LerpToRescuePoint(transform.position, targetPos));
;
    }

    private IEnumerator LerpToRescuePoint( Vector3 startPos, Vector3 targetPos)
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


    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            // 체력 감소 이벤트 넣기
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            animationHandler.SetLanding();
            Debug.Log("닿았다");
            baseState.isGrounded = true;
            baseState.isDoubleJump = false;
            animationHandler.SetRescue(false);
        }

    }

    protected virtual void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            baseState.isGrounded = false;
        }
    }
}
