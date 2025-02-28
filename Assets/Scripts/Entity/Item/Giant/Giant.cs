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
            SoundMananger.instance.PlayGiantEffect();
            BaseController playerController = other.GetComponent<BaseController>();
            
            if (playerController != null)
            {
                // SetBigger 메서드가 존재하는 경우 직접 호출
                playerController.SetBigger(duration); // 거대화

                // 이미 GiantMode가 활성화되었는지 확인하고, 없다면 활성화
                GiantMode giantMode = playerController.GetComponent<GiantMode>();
                if (giantMode == null)
                {
                    giantMode = playerController.gameObject.AddComponent<GiantMode>();
                    giantMode.ActivateGiantMode(playerController, duration);
                }
            }

            Destroy(gameObject); // 아이템 사용 후 제거
        }
    }
}
