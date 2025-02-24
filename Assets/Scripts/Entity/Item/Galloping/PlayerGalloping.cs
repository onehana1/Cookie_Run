using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGalloping : MonoBehaviour
{
    private BaseState baseState;
    private BackGroundController background; // 배경 컨트롤러 가져오기
    private bool isInvincible = false;

    private void Start()
    {
        baseState = GetComponent<BaseState>();
        background = FindObjectOfType<BackGroundController>(); // 배경 속도 컨트롤
    }

    public IEnumerator ActivateSpeedBoost(float duration, float multiplier)
    {
        Debug.Log("질주 시작!");

        float originalSpeed = baseState.moveSpeed;
        float originalBGSpeed = background.moveSpeed; // 기존 배경 속도 저장

        baseState.moveSpeed *= multiplier;
        background.moveSpeed *= multiplier; // 배경 속도 증가
        isInvincible = true;

        yield return new WaitForSeconds(duration);

        baseState.moveSpeed = originalSpeed;
        background.moveSpeed = originalBGSpeed; // 배경 속도 복구
        isInvincible = false;

        Debug.Log("질주 종료");
    }
}
