using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System.Collections;

public class PlayerGalloping : MonoBehaviour
{
    private BackGroundController background;
    private Invincibility invincibility;
    private BaseState baseState;

    private float originalBGSpeed; // 원래 배경 속도 저장
    private float originalMoveSpeed; // 원래 이동 속도 저장
    private bool isBoosting = false;

    public float dashMultiplier = 2f; // 기본 속도 배율
    public float dashDuration = 5f; // 기본 지속 시간

    private void Start()
    {
        background = FindObjectOfType<BackGroundController>();
        invincibility = GetComponent<Invincibility>();
        baseState = GetComponent<BaseState>();

        originalBGSpeed = background.moveSpeed;
        originalMoveSpeed = baseState.moveSpeed;
    }

    public void ActivateSpeedBoost(float duration, float multiplier)
    {
        if (isBoosting) return; // 이미 활성화된 경우 중복 실행 방지
        StartCoroutine(SpeedBoostCoroutine(duration, multiplier));
    }

    private IEnumerator SpeedBoostCoroutine(float duration, float multiplier)
    {
        isBoosting = true;
        Debug.Log("질주 시작!");

        // 배경 속도 및 이동 속도 증가, 무적 상태 활성화
        background.moveSpeed *= multiplier;
        baseState.SetMoveSpeed(baseState.moveSpeed * multiplier);
        invincibility.StartInvincibility(duration);

        yield return new WaitForSeconds(duration);

        // 원래 속도로 복귀
        background.moveSpeed = originalBGSpeed;
        baseState.SetMoveSpeed(originalMoveSpeed);

        isBoosting = false;
        Debug.Log("질주 종료");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (invincibility.IsInvincible && other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
            Debug.Log("장애물 파괴!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (invincibility.IsInvincible && collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
            Debug.Log("충돌한 장애물 파괴!");
        }
        else if (!invincibility.IsInvincible &&
                 (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Obstacle")))
        {
            baseState.TakeDamage(10f);
            Debug.Log("데미지를 입음!");
        }
    }
}