using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galloping : MonoBehaviour
{
    public float boostDuration = 5f; // 질주 지속 시간
    public float speedMultiplier = 2f; // 이동 속도 배율

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerGalloping playerGalloping = other.GetComponent<PlayerGalloping>();
            if (playerGalloping != null)
            {
                playerGalloping.ActivateSpeedBoost(boostDuration, speedMultiplier);
            }
            Destroy(gameObject); // 아이템 사용 후 제거
        }
    }
}
