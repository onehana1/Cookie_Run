using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image cooldownImage;
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
            cooldownTime = player.GetSkillCooldownTime();
            player.OnSkillUsed += HandleSkillUsed;
            StartCoroutine(UpdateCooldownUI());
        }
        rectTransform = GetComponent<RectTransform>();
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

    private IEnumerator UpdateCooldownUI()
    {
        while (true)
        {
            if (isSkillActive)
            {
                cooldownImage.color = new Color(cooldownImage.color.r, cooldownImage.color.g, cooldownImage.color.b, 0f); // 투명화
            }
            else if (player.IsSkillOnCooldown())
            {
                cooldownImage.color = new Color(cooldownImage.color.r, cooldownImage.color.g, cooldownImage.color.b, 1f); // 다시 보이기
                elapsedTime += Time.deltaTime;
                cooldownImage.fillAmount = elapsedTime / cooldownTime; // 0 → 1로 게이지 증가
            }
            else
            {
                elapsedTime = 0f;
                cooldownImage.fillAmount = 0f; // 쿨타임 끝나면 다시 초기화
            }

            yield return null;
        }
    }

    private void HandleSkillUsed(float cooldown)
    {
        isSkillActive = true; // 스킬 사용 시작
        StartCoroutine(HideCooldownUI(cooldown));
    }

    private IEnumerator HideCooldownUI(float duration)
    {
        cooldownImage.color = new Color(cooldownImage.color.r, cooldownImage.color.g, cooldownImage.color.b, 0f); // 투명화
        yield return new WaitForSeconds(duration); // 스킬 지속 시간 동안 대기
        isSkillActive = false;
    }

}
