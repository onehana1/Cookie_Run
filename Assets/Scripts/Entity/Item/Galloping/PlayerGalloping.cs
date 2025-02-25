using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGalloping : MonoBehaviour
{
    private BackGroundController background;
    private Invincibility invincibility;
    private BaseState baseState; // 체력 관리하는 BaseState
    private float originalBGSpeed;

    public float dashMultiplier = 2f;
    public float dashDuration = 2f;

    private void Start()
    {
        background = FindObjectOfType<BackGroundController>();
        invincibility = GetComponent<Invincibility>();
        baseState = GetComponent<BaseState>();
        originalBGSpeed = background.moveSpeed;
    }

    public void ActivateSpeedBoost()
    {
        StartCoroutine(SpeedBoostCoroutine());
    }

    private IEnumerator SpeedBoostCoroutine()
    {
        Debug.Log("질주 시작!");

        background.moveSpeed *= dashMultiplier;
        invincibility.StartInvincibility(dashDuration);

        yield return new WaitForSeconds(dashDuration);

        background.moveSpeed = originalBGSpeed;
        Debug.Log("질주 종료");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 무적 상태에서 장애물과 충돌하면 파괴
        if (invincibility.IsInvincible && other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
            Debug.Log("장애물 파괴!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 무적 상태라면 피격을 방지
        if (invincibility.IsInvincible)
        {
            Debug.Log("무적 상태이므로 데미지를 받지 않음");
            return;
        }

        // 적이나 장애물과 충돌 시 데미지 처리
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Obstacle"))
        {
            baseState.TakeDamage(10f); // 10 데미지 예시
            Debug.Log("데미지를 입음!");
        }
    }
}
