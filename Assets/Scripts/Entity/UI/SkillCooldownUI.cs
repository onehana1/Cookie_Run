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
            if (player.IsSkillOnCooldown())  // �÷��̾��� ��ų ��Ÿ�� Ȯ��
            {
                elapsedTime += Time.deltaTime;
                cooldownImage.fillAmount = elapsedTime / cooldownTime; // 0 �� 1�� ������ ����
            }
            else
            {
                elapsedTime = 0f;
                cooldownImage.fillAmount = 0f; // ��Ÿ���� ������ �ٽ� �ʱ�ȭ
            }

            yield return null;
        }
    }
}
