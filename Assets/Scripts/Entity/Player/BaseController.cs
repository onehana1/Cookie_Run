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

    [Header("Character")]
    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] float jumpForce = 10.0f;
    [Header("Time")]
    [SerializeField] float hitTime = 0.5f;
    [SerializeField] float invinvibleTime = 3.0f;
    [SerializeField] float blinkIntervalTime = 0.5f;    
    [SerializeField] float rescueTime = 2.0f;
    [SerializeField] float rescueLerpTime = 0.5f;


    private bool isGrounded = true;
    private bool isDoubleJump = false;
    private bool isSliding = false;
    private bool isHit = false;
    private bool isLive = true;
    private bool isRescue = false;

    [Header("Test")]
    [SerializeField] private bool isInvincible = false;
    [SerializeField] float groundY = -1.5f;
    [SerializeField] float returnGroundY = 2.0f;



    protected bool isFastRunning = false;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        if (isHit) return;
        HandleAction();

        //if (isInvincible)   // 무적시간동안 안떨어지게..
        //{
        //    transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        //}

        if ( transform.position.y < groundY && isLive)
        {
            if (!isRescue)
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
            if (isGrounded)
            {
                Jump(); // 첫 번째 점프
            }
            else if (isDoubleJump)
            {
                DoubleJump(); // 공중에서 한 번만 더 점프 가능
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            StartSlide();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            EndSlide();
        }

        if (!isGrounded && rb.velocity.y <= 0)
        {
            animationHandler.SetFalling(true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            isFastRunning = !isFastRunning; // 상태 토글
            float newSpeed = isFastRunning ? 0.0f : 1.0f;
            animationHandler.SetRunning(newSpeed);
        }

        if (Input.GetKeyDown(KeyCode.H) && !isHit)
        {
            TakeHit();
        }

        if (Input.GetKeyDown(KeyCode.D))    // 테스트
        {
            Die();
        }

        if (Input.GetKeyDown(KeyCode.I))    // 테스트
        {
            StartInvincible();
        }
    }


    protected virtual void Move()
    {
        // 캐릭터는 무조건 앞으로가는 모션"만" 함 

    }

    protected virtual void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(0f, jumpForce);
            isGrounded = false; 
            isDoubleJump = true;
            animationHandler.SetJumping();
        }
    }

    protected virtual void DoubleJump()
    {
        if (!isDoubleJump) return;

        rb.velocity = new Vector2(0f, jumpForce);
        isDoubleJump = false; 
        animationHandler.SetDoubleJump();
    }

    private void StartSlide()
    {
        if (!isGrounded) return; // 공중에서 슬라이드 x

        isSliding = true;
        animationHandler.SetSlide(true);
    }

    private void EndSlide()
    {
        isSliding = false;
        animationHandler.SetSlide(false);
    }


    private void StartInvincible()
    {
        if (isInvincible) return;
        Debug.Log("무적쿠키가 간다");
        isInvincible = true;
        StartCoroutine(BlinkEffect(invinvibleTime));
        StartCoroutine(EndInvincible());    
    }

    private IEnumerator BlinkEffect(float BlinkTime)
    {
        float time = 0f;

        while(time < BlinkTime)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(blinkIntervalTime);
            spriteRenderer.color = new Color(1, 1, 1, 1.0f);
            yield return new WaitForSeconds(blinkIntervalTime);
            time += blinkIntervalTime * 2;


        }
    }

    private IEnumerator EndInvincible()
    {

        yield return new WaitForSeconds(invinvibleTime);
        isInvincible = false;
        spriteRenderer.color = new Color(1,1,1,1);

    }



    private void TakeHit()
    {
        if (isHit || isInvincible) return;

        isHit = true;
        animationHandler.SetHit(true);

        StartCoroutine(ResetHitState());

    }
    private IEnumerator ResetHitState() // OnStateExit vs 코루틴 생각하다가 피격 애니메이션 1프레임 고정이라 코루틴 사용
    {
        yield return new WaitForSeconds(hitTime); // 피격 애니메이션 길이만큼 대기
        isHit = false;

        animationHandler.SetHit(false);
        animationHandler.SetRunning(1.0f);  // 다시 뛰는 애니메이션
    }

    private void Die()
    {
        isLive = false;

        if(!isLive)
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
        isRescue = true;
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
        yield return new WaitForSeconds(rescueTime);
        StartCoroutine(RescueToLand());
    }

    private IEnumerator RescueToLand()
    {
        if (isGrounded) yield return null;
        isRescue = false;
        animationHandler.SetRescue(false);
        rb.gravityScale = 1.0f;
        Debug.Log("구해줬어요");

        StartInvincible();
        yield return null;
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
            isGrounded = true;
            isDoubleJump = false;
            animationHandler.SetRescue(false);
        }

    }

    protected virtual void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
