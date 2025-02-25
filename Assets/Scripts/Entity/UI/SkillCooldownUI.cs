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

    [SerializeField] private Vector3 offset = new Vector3(0, 0.0f, 0); // �Ӹ� �� ��ġ ����


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

        // ���� �����̽� ĵ�������� UI�� �÷��̾� �Ӹ� ���� ��ġ��Ŵ
        Vector3 worldPosition = playerTransform.position + offset;
        rectTransform.position = worldPosition;

        // UI�� �׻� ī�޶� �ٶ󺸵��� ����
        rectTransform.LookAt(mainCamera.transform);
        rectTransform.Rotate(0, 180f, 0); // LookAt�� �⺻ ������ �ݴ� �����̹Ƿ� ����
    }

    private IEnumerator UpdateCooldownUI()
    {
        while (true)
        {
            if (isSkillActive)
            {
                cooldownImage.color = new Color(cooldownImage.color.r, cooldownImage.color.g, cooldownImage.color.b, 0f); // ����ȭ
            }
            else if (player.IsSkillOnCooldown())
            {
                cooldownImage.color = new Color(cooldownImage.color.r, cooldownImage.color.g, cooldownImage.color.b, 1f); // �ٽ� ���̱�
                elapsedTime += Time.deltaTime;
                cooldownImage.fillAmount = elapsedTime / cooldownTime; // 0 �� 1�� ������ ����
            }
            else
            {
                elapsedTime = 0f;
                cooldownImage.fillAmount = 0f; // ��Ÿ�� ������ �ٽ� �ʱ�ȭ
            }

            yield return null;
        }
    }

    private void HandleSkillUsed(float cooldown)
    {
        isSkillActive = true; // ��ų ��� ����
        StartCoroutine(HideCooldownUI(cooldown));
    }

    private IEnumerator HideCooldownUI(float duration)
    {
        cooldownImage.color = new Color(cooldownImage.color.r, cooldownImage.color.g, cooldownImage.color.b, 0f); // ����ȭ
        yield return new WaitForSeconds(duration); // ��ų ���� �ð� ���� ���
        isSkillActive = false;
    }

}
