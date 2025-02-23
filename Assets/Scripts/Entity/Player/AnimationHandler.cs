using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{

    protected Animator animator;  

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetRunning(float speed)
    {
        animator.SetFloat("speed", speed); // 블렌드 트리의 speed 값을 변경
    }
    public void SetJumping()
    {
        animator.SetTrigger("Jump");
    }

    public void SetDoubleJump()
    {
        animator.SetTrigger("DoubleJump");
    }

    public void SetSlide(bool isSliding)
    {
        animator.SetBool("isSliding", isSliding);
    }


    public void SetFalling(bool isFalling)
    {
        animator.SetBool("isFalling", isFalling);
    }

    public void SetLanding()
    {
        animator.SetTrigger("Land");
        animator.SetBool("isFalling", false); 
    }

    public void SetHit(bool isHitting)
    {
        animator.SetBool("isHit", isHitting);
    }

}
