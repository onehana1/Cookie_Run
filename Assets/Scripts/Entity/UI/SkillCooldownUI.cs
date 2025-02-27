using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image cooldownImage;
   // [SerializeField] private Image background;

    [SerializeField] private ScholarController player;
    [SerializeField] private Transform playerTransform;


    private float cooldownTime;
    private float elapsedTime;
    private bool isSkillActive = false;
    private Camera mainCamera;
    private RectTransform rectTransform;

    [SerializeField] private Vector3 offset = new Vector3(0, 0.0f, 0); // 머리 위 위치 조정


    private void Start()
    {
        if (player != null)
        {
            cooldownTime = player.GetQuizCooldownTime();    // 이거 개선 가능할 것 같은데... 일단 퀴즈 쿨타임으로 고정
            player.OnQuizUsed += HandleSkillUsed;
        }
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(UpdateCooldownUI());

        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (playerTransform == null || mainCamera == null) return;

        // 월드 스페이스 캔버스에서 UI를 플레이어 머리 위로 위치시킴
        Vector3 worldPosition = playerTransform.position + offset;
        rectTransform.position = worldPosition;

        // UI가 항상 카메라를 바라보도록 설정
        rectTransform.LookAt(mainCamera.transform);
        rectTransform.Rotate(0, 180f, 0); // LookAt의 기본 동작이 반대 방향이므로 보정
    }

    private IEnumerator UpdateCooldownUI()  // 얘가 스킬 끝나자 마자 실행되길 바라는데 지금 안됨
    {
        while (true)
        {
            if (!isSkillActive)
            {
                cooldownImage.color = new Color(cooldownImage.color.r, cooldownImage.color.g, cooldownImage.color.b, 1f);

                elapsedTime += Time.deltaTime; // 시간 증가
                cooldownImage.fillAmount = elapsedTime / cooldownTime; // 0 -> 1로 증가

                if (elapsedTime >= cooldownTime)
                {
                    elapsedTime = 0;
                    cooldownImage.fillAmount = 0f;
                    cooldownImage.color = new Color(cooldownImage.color.r, cooldownImage.color.g, cooldownImage.color.b, 0f);
                }
            }

            yield return null;
        }
    }


    private void HandleSkillUsed(float cooldown)
    {
        isSkillActive = false; // 스킬 사용 시작
        cooldownTime = cooldown;
        StartCoroutine(HideCooldownUI(cooldown));
    }


    private IEnumerator HideCooldownUI(float duration)
    {
        cooldownImage.color = new Color(cooldownImage.color.r, cooldownImage.color.g, cooldownImage.color.b, 0f); // 투명화
     //   background.color = new Color(0, 0, 0, 0f); // 투명화
         elapsedTime = 0f; 
        yield return new WaitForSeconds(duration); // 스킬 지속 시간 동안 대기
        isSkillActive = false;
    }

}
