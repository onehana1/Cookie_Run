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
        // UIManager�� �̺�Ʈ�� �����Ͽ� ��ư ���¸� �ڵ� ����
        UIManager.Instance.OnQuizModeChanged += UpdateButtonState;

        oButton.onClick.AddListener(() => UIManager.Instance.SelectOXAnswer(true));
        xButton.onClick.AddListener(() => UIManager.Instance.SelectOXAnswer(false));

        UpdateButtonState(false); // �ʱ� ��ư ���� ����
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
        // ���� ���� Ȯ�� ��û

    }
    private void UpdateButtonState(bool isQuizActive)
    {
        // ���� ����̸� OX ��ư�� Ȱ��ȭ
        oButton.gameObject.SetActive(isQuizActive);
        xButton.gameObject.SetActive(isQuizActive);
        jumpButton.gameObject.SetActive(!isQuizActive);
        slideButton.gameObject.SetActive(!isQuizActive);
    }

}
