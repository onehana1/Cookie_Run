using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseJelly : MonoBehaviour
{
    public int scoreValue; // 젤리를 먹으면 얻는 점수

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 충돌한 객체가 Player인지 확인
        {
            SoundMananger.instance.PlayJellyEffect();
            Destroy(gameObject); // 젤리 삭제
            PlayManager.Instance.AddScore(scoreValue); // 점수 추가
        }

        else if (other.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
