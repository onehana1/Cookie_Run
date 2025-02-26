using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpBotton : MonoBehaviour
{
    public BaseController baseController;

    public void JumpBottonClick()
    {
        if (baseController.baseState.isDead) return;
        if (!baseController.baseState.isJump)
            baseController.Jump();
        else if (!baseController.baseState.isDoubleJump)
            baseController.DoubleJump();
    }

    public void SlideBottonDown()
    {
        if (baseController.baseState.isDead) return;

        if (baseController.baseState.isGrounded)
            baseController.StartSlide();
    }

    public void OnPointerUp()
    {
        if (baseController.baseState.isDead) return;

        Debug.Log("OnPointerUp");
        baseController.EndSlide();
    }

    public void OnPointerDown()
    {
        if (baseController.baseState.isDead) return;

        Debug.Log("OnPointerDown");
        if (baseController.baseState.isGrounded)
            baseController.StartSlide();
    }
}
