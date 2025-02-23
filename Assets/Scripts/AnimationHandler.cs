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

    public void SetRunning(bool isRunning)
    {
        //animator.SetBool("isRunning", isRunning);
    }
    public void SetJumping(bool isJumping)
    {
        //animator.SetBool("isJumping", isJumping);
    }
    public void SetDoubleJump(bool isJumping)
    {
        //animator.SetBool("isJumping", isJumping);

    }

    public void SetSlide(bool isSliding)
    {
        //animator.SetBool("isSliding", isSliding);
    }

    public void SetLanding(bool isLanding)
    {
        //animator.SetBool("isLanding", isLanding);
    }




}
