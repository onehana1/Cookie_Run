using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galloping : MonoBehaviour
{
    public float boostDuration = 2.0f; // 질주 지속 시간
    public float speedMultiplier = 2.5f; // 속도 증가 배율

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어가 아이템을 먹으면
        {
            PlayerGalloping player = other.GetComponent<PlayerGalloping>();
            if (player != null)
            {
                player.StartCoroutine(player.ActivateSpeedBoost(boostDuration, speedMultiplier)); // 질주 효과 활성화
            }

            Destroy(gameObject); // 아이템 제거
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
