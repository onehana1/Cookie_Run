using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGalloping : MonoBehaviour
{
    private BaseState baseState; // 플레이어의 이동 속도를 관리하는 BaseState
    private BackGroundController background; // 배경 속도를 제어하는 컨트롤러
    private bool isInvincible = false; // 무적 상태 여부 (질주 중 무적)

    private void Start()
    {
        // BaseState 가져오기 (플레이어 이동 속도를 제어)
        baseState = GetComponent<BaseState>();

        // 씬에서 BackGroundController 찾기 (배경 속도를 조절하기 위함)
        background = FindObjectOfType<BackGroundController>();
    }

    /// <summary>
    /// 질주 모드 활성화: 일정 시간 동안 속도를 증가시키고 무적 상태 적용
    /// </summary>
    /// <param name="duration">질주 지속 시간</param>
    /// <param name="multiplier">속도 증가 배율</param>
    public IEnumerator ActivateSpeedBoost(float duration, float multiplier)
    {
        Debug.Log("질주 시작!");

        
        float originalSpeed = baseState.moveSpeed; // 현재 속도 저장 (질주 끝난후 원래 속도 저장)
        float originalBGSpeed = background.moveSpeed; // 기존 배경 속도 저장

        // 이동 속도 및 배경 속도 증가
        baseState.moveSpeed *= multiplier;
        background.moveSpeed *= multiplier;
        isInvincible = true; // 무적 상태 활성화

        // 지정된 시간(duration)만큼 대기
        yield return new WaitForSeconds(duration);

        // 속도 및 배경 속도를 원래대로 복구
        baseState.moveSpeed = originalSpeed;
        background.moveSpeed = originalBGSpeed;
        isInvincible = false; // 무적 상태 해제

        Debug.Log("질주 종료");
    }
}
