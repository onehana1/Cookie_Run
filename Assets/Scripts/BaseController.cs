using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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


    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] float jumpForce = 10.0f;
    [SerializeField] float invinvibleTiem = 2.0f;


    private bool isGrounded = true;
    private bool isDoubleJump = false;
    private bool isSliding = false;
    private bool isJump = false;
    private bool isLanding = false;


    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump(); // 첫 번째 점프
            }
            else if (!isDoubleJump)
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
    }

    protected virtual void HandleAction()
    {

    }

    protected virtual void Move()
    {
        // 캐릭터는 무조건 앞으로가는 모션"만" 함 

    }

    protected virtual void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2 (0f, jumpForce);
            isGrounded = false; // 점프했으므로 착지 상태 해제
            isJump = true; // 점프 상태 활성화
            // animationHandler.SetJumping(); 
        }
    }

    protected virtual void DoubleJump()
    {
        if (!isDoubleJump) // 한 번만 더블 점프 가능
        {
            rb.velocity = new Vector2(0f, jumpForce);
            isDoubleJump = true; // 더블 점프 후에는 다시 점프 불가능
            // animationHandler.SetDoubleJump();
        }
    }

    private void StartSlide()
    {
        if (isJump) return;
        isSliding = true;
        // animationHandler.SetSlide(isSliding);
        rb.velocity = new Vector2(0f, 0f);
        
    }

    private void EndSlide()
    {
        isSliding = false;
        //animationHandler.SetSlide(isSliding);
    }



    private void Knockback()
    {
       
    }

    private void Invincible()
    {
        
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            // 체력 감소 이벤트 넣기
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // 바닥에 닿으면 착지 상태
            isJump = false;
            isDoubleJump = false; // 착지하면 다시 더블 점프 가능하도록 설정
            // animationHandler.SetLanding(isLanding);
        }

    }

    protected virtual void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // 바닥에서 떨어졌으므로 공중 상태
        }
    }




}
