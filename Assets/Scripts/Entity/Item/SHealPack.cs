using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHealPack : MonoBehaviour
{
    public float healAmount = 20f; // 회복량

    private void OnTriggerEnter2D(Collider2D other) // 플레이어가 충돌하면
    {
        if (other.CompareTag("Player")) // 태그 "Player"
        {
            BaseState baseState = other.GetComponent<BaseState>(); // 플레이어의 체력 스크립트 가져오기
            if (baseState != null)
            {
                //baseState.hp(healAmount); // 체력 회복
            }

            Destroy(gameObject); // 힐팩 아이템 삭제
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
