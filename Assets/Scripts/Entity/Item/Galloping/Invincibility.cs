using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Invincibility : MonoBehaviour
{
 
    private bool isInvincible = false; // 무적 상태 여부
    public bool IsInvincible => isInvincible; // 외부에서 확인 가능

    public void StartInvincibility(float duration)
    {
        if (isInvincible) return; // 중복 실행 방지
        StartCoroutine(InvincibilityCoroutine(duration));
    }

    private IEnumerator InvincibilityCoroutine(float duration)
    {
        isInvincible = true;
        Debug.Log("무적 상태 ON");

        yield return new WaitForSeconds(duration);

        isInvincible = false;
        Debug.Log("무적 상태 OFF");
    }
}