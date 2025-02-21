using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyController : MonoBehaviour
{
    public int scoreValue = 10; // 젤리를 먹으면 얻는 점수

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어가 충돌했는지 확인
        {
            GameManager.Instance.AddScore(scoreValue); // 점수 추가
            Destroy(gameObject); // 젤리 삭제
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
