using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGalloping : MonoBehaviour
{
    private BackGroundController background; // 배경 속도 컨트롤러
    private bool isInvincible = false; // 무적 상태 여부
    private float originalBGSpeed; // 기존 배경 속도 저장

    public float dashMultiplier = 2f; // 질주 속도 배율 (2배)
    public float dashDuration = 2f; // 질주 지속 시간 (초)

    private void Start()
    {
        background = FindObjectOfType<BackGroundController>(); // 배경 컨트롤러 찾기
        originalBGSpeed = background.moveSpeed; // 원래 속도 저장
    }

    public void ActivateSpeedBoost()
    {
        StartCoroutine(SpeedBoostCoroutine());
    }

    private IEnumerator SpeedBoostCoroutine()
    {
        Debug.Log("질주 시작!");

        // 배경 속도 증가 & 무적 상태 활성화
        background.moveSpeed *= dashMultiplier;
        isInvincible = true;

        yield return new WaitForSeconds(dashDuration); // 일정 시간 동안 유지

        // 배경 속도 원상 복구 & 무적 상태 해제
        background.moveSpeed = originalBGSpeed;
        isInvincible = false;

        Debug.Log("질주 종료");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 질주 상태에서 장애물과 충돌하면 파괴
        if (isInvincible && other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject); // 장애물 제거
            //ScoreManager.Instance.AddScore(100); // 점수 추가
            //Debug.Log("장애물 파괴! +100점");

        }
    }
}
