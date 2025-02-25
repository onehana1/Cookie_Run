using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galloping : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어가 아이템을 먹으면
        {
            PlayerGalloping playerGalloping = other.GetComponent<PlayerGalloping>();
            if (playerGalloping != null)
            {
                playerGalloping.ActivateSpeedBoost(); // 질주 시작
            }

            Destroy(gameObject); // 아이템 제거

        }
    }
}
