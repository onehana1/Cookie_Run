using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System.Collections;

public class PlayerGalloping : MonoBehaviour
{
    private BackGroundController background;
    private BaseState baseState;
    private PlayManager playManager;

    private float originalBGSpeed;
    private float originalMoveSpeed;
    private bool isBoosting = false;

    public float dashMultiplier = 2f;
    public float dashDuration = 5f;

    private void Start()
    {
        background = FindObjectOfType<BackGroundController>();
        baseState = GetComponent<BaseState>();
        playManager = PlayManager.Instance;

        originalBGSpeed = background.moveSpeed;
        originalMoveSpeed = baseState.moveSpeed;
    }

    public void ActivateSpeedBoost(float duration, float multiplier)
    {
        if (isBoosting) return;
        StartCoroutine(SpeedBoostCoroutine(duration, multiplier));
    }

    private IEnumerator SpeedBoostCoroutine(float duration, float multiplier)
    {
        isBoosting = true;
        Debug.Log("질주 시작!");

        // 배경 속도 및 이동 속도 증가, HP 감소 방지
        background.moveSpeed *= multiplier;
        baseState.SetMoveSpeed(baseState.moveSpeed * multiplier);
        float originalHp = playManager.hp;

        yield return new WaitForSeconds(duration);

        // 원래 속도로 복귀 
        background.moveSpeed = originalBGSpeed;
        baseState.SetMoveSpeed(originalMoveSpeed);
        playManager.hp = originalHp; // HP를 원래 값으로 복구

        isBoosting = false;
        Debug.Log("질주 종료");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isBoosting && other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
            Debug.Log("장애물 파괴!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBoosting && collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
            Debug.Log("충돌한 장애물 파괴!");
        }
    }
}