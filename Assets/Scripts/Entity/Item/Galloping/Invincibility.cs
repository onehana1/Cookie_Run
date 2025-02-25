using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isInvincible = false; // 무적 상태 여부

    public bool IsInvincible => isInvincible; // 외부에서 무적 상태 확인 가능

    // 무적 상태를 시작하는 메소드
    public void StartInvincibility(float duration)
    {
        if (isInvincible) return; // 이미 무적 상태라면 중복 실행 방지
        StartCoroutine(InvincibilityCoroutine(duration));
    }

    private IEnumerator InvincibilityCoroutine(float duration)
    {
        isInvincible = true; // 무적 활성화
        Debug.Log("무적 상태 ON");

        yield return new WaitForSeconds(duration); // 일정 시간 동안 유지

        isInvincible = false; // 무적 해제
        Debug.Log("무적 상태 OFF");
    }
}
