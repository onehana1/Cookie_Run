using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float magnetDuration = 5f; // 자석 효과 지속 시간
    public float magnetRange = 5f; // 자석 범위
    public float attractionSpeed = 5f; // 아이템이 빨려오는 속도

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어가 자석을 먹으면
        {
            SoundMananger.instance.PlayItemEffect();
            StartCoroutine(ActivateMagnet(other.gameObject)); // 자석 효과 활성화
            Destroy(gameObject); // 자석 아이템 삭제
        }
    }

    private IEnumerator ActivateMagnet(GameObject player)
    {
        MagnetEffect magnet = player.GetComponent<MagnetEffect>();
        if (magnet == null)
        {
            magnet = player.AddComponent<MagnetEffect>(); // 플레이어에게 MagnetEffect 추가
        }

        magnet.EnableMagnet(magnetRange, attractionSpeed, magnetDuration); // 자석 효과 적용
        yield return new WaitForSeconds(magnetDuration); // 5초 후
        //magnet.DisableMagnet(); // 자석 효과 해제
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
