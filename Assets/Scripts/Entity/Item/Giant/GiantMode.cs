using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantMode : MonoBehaviour
{
    private BaseController playerController;
    private Collider2D playerCollider;
    private bool isGiantActive = false;

    public void ActivateGiantMode(BaseController controller, float duration)
    {
        if (isGiantActive) return; // 이미 활성화된 경우 중복 실행 방지

        isGiantActive = true;
        playerController = controller;
        playerCollider = controller.GetComponent<Collider2D>();

        // 무적 상태 활성화
        playerController.baseState.StartInvincibility(duration);

        // 장애물 파괴 기능 활성화
        // playerCollider.isTrigger = true;

        // 일정 시간 후 원래 상태로 복구
        StartCoroutine(DisableGiantMode(duration));
    }

    private IEnumerator DisableGiantMode(float duration)
    {
        yield return new WaitForSeconds(duration);

        // 원래 상태로 복구
        // playerCollider.isTrigger = false;
        isGiantActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isGiantActive && other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject); // 장애물 파괴
        }
    }
}