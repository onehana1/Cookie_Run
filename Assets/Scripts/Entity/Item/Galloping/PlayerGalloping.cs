using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PlayerGalloping : MonoBehaviour
{
    // 배경 및 플레이어 상태 관련 변수
    private BackGroundController background;
    private BaseState baseState;
    private PlayManager playManager;

    // 원래 속도를 저장할 변수
    private float originalBGSpeed;
    private float originalMoveSpeed;
    private bool isBoosting = false; // 질주 중인지 여부

    public float dashMultiplier = 2f; // 질주 시 속도 배율
    public float dashDuration = 5f; // 질주 지속 시간

    private void Start()
    {
        // 필요한 컴포넌트 및 매니저 참조 가져오기
        background = FindObjectOfType<BackGroundController>();
        baseState = GetComponent<BaseState>();
        playManager = PlayManager.Instance;

        // 원래 속도 저장
        originalBGSpeed = background.moveSpeed;
        originalMoveSpeed = baseState.moveSpeed;
    }

    // 질주Tiem 기능 활성화
    public void ActivateSpeedBoost(float duration, float multiplier)
    {
        if (isBoosting) return; // 이미 질주 중이면 실행 안 함
        StartCoroutine(SpeedBoostCoroutine(duration, multiplier));
    }

    // 질주Tiem 코루틴 실행
    private IEnumerator SpeedBoostCoroutine(float duration, float multiplier)
    {
        isBoosting = true;
        Debug.Log("질주 시작!");

        // 배경 및 플레이어 속도 증가
        background.moveSpeed *= multiplier;
        baseState.SetMoveSpeed(baseState.moveSpeed * multiplier);

        // 무적 상태 활성화
        baseState.StartInvincibility(duration);
        StartCoroutine(BlinkEffect(duration)); // 깜빡이는 효과 추가

        yield return new WaitForSeconds(duration);

        // 원래 속도로 복귀
        background.moveSpeed = originalBGSpeed;
        baseState.SetMoveSpeed(originalMoveSpeed);

        isBoosting = false;
        Debug.Log("질주 종료");
    }

    // 장애물과 충돌했을 때 파괴하는 기smd
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isBoosting && other.CompareTag("Obstacle"))
        {
            SoundMananger.instance.PlayDestroyEffect();
            Destroy(other.gameObject);
            Debug.Log("장애물 파괴!");
        }
    }

    // 장애물과 충돌했을 때 파괴하는 기능
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBoosting && collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
            Debug.Log("충돌한 장애물 파괴!");
        }
    }

    // 무적 상태일 때 깜빡이는 효과 추가
    private IEnumerator BlinkEffect(float duration)
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        float time = 0f;
        float blinkIntervalTime = 0.2f; // 깜빡이는 간격 설정

        while (time < duration)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f); // 반투명 상태
            yield return new WaitForSeconds(blinkIntervalTime);
            spriteRenderer.color = new Color(1, 1, 1, 1.0f); // 원래 상태
            yield return new WaitForSeconds(blinkIntervalTime);
            time += blinkIntervalTime * 2;
        }

        spriteRenderer.color = new Color(1, 1, 1, 1); // 원래 색상 복원
    }
}