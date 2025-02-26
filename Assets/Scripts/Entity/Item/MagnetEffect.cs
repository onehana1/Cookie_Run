using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetEffect : MonoBehaviour
{
    private float range; // 자석 범위
    private float speed; // 빨려오는 속도
    private bool isMagnetActive = false; // 자석 효과 켜짐 여부

    public void EnableMagnet(float magnetRange, float attractionSpeed, float duration)
    {
        range = magnetRange; // 자석 범위 설정
        speed = attractionSpeed; // 빨려오는 속도 설정
        isMagnetActive = true; // 자석 활성화
        StartCoroutine(DisableMagnet(duration));    
    }
    //public void DisableMagnet()
    //{
    //    isMagnetActive = false; // 자석 효과 비활성화
    //}
    private void Update()
    {
        if (!isMagnetActive) return; // 자석이 활성화되지 않았다면 아무것도 안 함

        Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D item in items)
        {
            if (item.CompareTag("Item")) // 아이템 태그가 있는 경우
            {
                // 아이템을 플레이어 방향으로 이동
                item.transform.position = Vector2.Lerp(item.transform.position, transform.position, speed * Time.deltaTime);
            }
        }
    }

    IEnumerator DisableMagnet(float duration)
    {
        yield return new WaitForSeconds(duration);
        isMagnetActive = false;
    }
}
