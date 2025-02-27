using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : MonoBehaviour
{
    public float duration = 20f; // 거대화 지속 시간

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어가 아이템을 먹으면
        {
            BaseController playerController = other.GetComponent<BaseController>();

            if (playerController != null)
            {
                playerController.SendMessage("SetBigger", duration); // 거대화

                // GiantMode 컴포넌트 추가 및 활성화
                GiantMode giantMode = playerController.gameObject.AddComponent<GiantMode>();
                giantMode.ActivateGiantMode(playerController, duration);
            }

            Destroy(gameObject); // 아이템 사용 후 제거
        }
    }
}
