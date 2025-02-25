using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image cooldownImage;
    [SerializeField] private ScholarController player;

    private float cooldownTime;
    private float elapsedTime;

    private void Start()
    {
        if (player != null)
        {
            cooldownTime = player.GetSkillCooldownTime();
            StartCoroutine(UpdateCooldownUI());
        }
    }

    private IEnumerator UpdateCooldownUI()
    {
        while (true)
        {
            if (player.IsSkillOnCooldown())  // 플레이어의 스킬 쿨타임 확인
            {
                elapsedTime += Time.deltaTime;
                cooldownImage.fillAmount = elapsedTime / cooldownTime; // 0 → 1로 게이지 증가
            }
            else
            {
                elapsedTime = 0f;
                cooldownImage.fillAmount = 0f; // 쿨타임이 끝나면 다시 초기화
            }

            yield return null;
        }
    }
}
