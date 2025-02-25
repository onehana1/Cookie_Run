using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGalloping : MonoBehaviour
{
    private BackGroundController background; // 배경 속도 컨트롤러
    private Invincibility invincibility; // 무적 상태 관리 클래스
    private BaseState baseState; // 체력 및 캐릭터 상태 관리 클래스
    private float originalBGSpeed; // 원래 배경 속도 저장

    public float dashMultiplier = 2f; // 질주 시 배경 속도 배율 (기본 2배)
    public float dashDuration = 7f; // 질주 지속 시간 (초)

    private void Start()
    {
        // 해당 오브젝트에서 필요한 컴포넌트들을 가져옴
        background = FindObjectOfType<BackGroundController>(); // 배경 속도 제어
        invincibility = GetComponent<Invincibility>(); // 무적 관리
        baseState = GetComponent<BaseState>(); // 캐릭터 상태 관리

        // 현재 배경 속도 저장
        originalBGSpeed = background.moveSpeed;
    }

    // 질주(대시) 시작 함수
    public void ActivateSpeedBoost()
    {
        StartCoroutine(SpeedBoostCoroutine());
    }

    // 질주 지속 효과 코루틴
    private IEnumerator SpeedBoostCoroutine()
    {
        Debug.Log("질주 시작!");

        // 배경 속도 증가 및 무적 상태 활성화
        background.moveSpeed *= dashMultiplier;
        invincibility.StartInvincibility(dashDuration); // 일정 시간 동안 무적

        // 질주 지속 시간 동안 대기
        yield return new WaitForSeconds(dashDuration);

        // 배경 속도를 원래대로 복구
        background.moveSpeed = originalBGSpeed;

        Debug.Log("질주 종료");
    }

    // 무적 상태에서 장애물 파괴
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 무적 상태일 때만 장애물 파괴
        if (invincibility.IsInvincible && other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
            
        }
    }

    // 충돌 감지 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 무적 상태일 경우 피격 로직 무시
        if (invincibility.IsInvincible)
        {
            Debug.Log("무적 상태이므로 데미지를 받지 않음");
            return;
        }
    }
}
