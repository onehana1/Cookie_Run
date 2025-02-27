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

    [SerializeField] private Vector3 offset = new Vector3(0, 0.0f, 0); // �Ӹ� �� ��ġ ����


    private void Start()
    {
        if (player != null)
        {
            cooldownTime = player.GetQuizCooldownTime();    // �̰� ���� ������ �� ������... �ϴ� ���� ��Ÿ������ ����
            player.OnQuizUsed += HandleSkillUsed;
        }
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(UpdateCooldownUI());

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

    private IEnumerator UpdateCooldownUI()  // �갡 ��ų ������ ���� ����Ǳ� �ٶ�µ� ���� �ȵ�
    {
        while (true)
        {
            if (!isSkillActive)
            {
                cooldownImage.color = new Color(cooldownImage.color.r, cooldownImage.color.g, cooldownImage.color.b, 1f);

                elapsedTime += Time.deltaTime; // �ð� ����
                cooldownImage.fillAmount = elapsedTime / cooldownTime; // 0 -> 1�� ����

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
        isSkillActive = false; // ��ų ��� ����
        cooldownTime = cooldown;
        StartCoroutine(HideCooldownUI(cooldown));
    }


    private IEnumerator HideCooldownUI(float duration)
    {
        cooldownImage.color = new Color(cooldownImage.color.r, cooldownImage.color.g, cooldownImage.color.b, 0f); // ����ȭ
     //   background.color = new Color(0, 0, 0, 0f); // ����ȭ
         elapsedTime = 0f; 
        yield return new WaitForSeconds(duration); // ��ų ���� �ð� ���� ���
        isSkillActive = false;
    }

}
