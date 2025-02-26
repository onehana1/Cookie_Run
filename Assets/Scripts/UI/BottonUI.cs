using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BottonUI : MonoBehaviour
{
    public BaseController baseController;
    private bool isQuizMode = false;

    [Header("UI Elements")]
    public Button jumpButton;
    public Button slideButton;
    public Button oButton;
    public Button xButton;

    private void Start()
    {
        // UIManager의 이벤트를 구독하여 버튼 상태를 자동 변경
        UIManager.Instance.OnQuizModeChanged += UpdateButtonState;

        oButton.onClick.AddListener(() => UIManager.Instance.SelectOXAnswer(true));
        xButton.onClick.AddListener(() => UIManager.Instance.SelectOXAnswer(false));

        UpdateButtonState(false); // 초기 버튼 상태 설정
    }


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

    private void SelectAnswer(bool isO)
    {
        // 퀴즈 정답 확인 요청

    }
    private void UpdateButtonState(bool isQuizActive)
    {
        // 퀴즈 모드이면 OX 버튼만 활성화
        oButton.gameObject.SetActive(isQuizActive);
        xButton.gameObject.SetActive(isQuizActive);
        jumpButton.gameObject.SetActive(!isQuizActive);
        slideButton.gameObject.SetActive(!isQuizActive);
    }

}
