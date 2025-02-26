using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHealPack : MonoBehaviour
{
    public float healAmount = 20f; // 회복량

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어와 충돌했을 때
        {
            BaseState baseState = other.GetComponent<BaseState>(); // 플레이어의 BaseState 가져오기

            if (baseState != null)
            {
                baseState.Heal(healAmount); // 체력 회복
                Debug.Log("체력 회복: " + healAmount);
            }

            Destroy(gameObject); // 아이템 삭제
        }
    }

}
