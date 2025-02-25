using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGalloping : MonoBehaviour
{
    private BackGroundController background; // ��� �ӵ� ��Ʈ�ѷ�
    private bool isInvincible = false; // ���� ���� ����
    private float originalBGSpeed; // ���� ��� �ӵ� ����

    public float dashMultiplier = 2f; // ���� �ӵ� ���� (2��)
    public float dashDuration = 2f; // ���� ���� �ð� (��)

    private void Start()
    {
        background = FindObjectOfType<BackGroundController>(); // ��� ��Ʈ�ѷ� ã��
        originalBGSpeed = background.moveSpeed; // ���� �ӵ� ����
    }

    public void ActivateSpeedBoost()
    {
        StartCoroutine(SpeedBoostCoroutine());
    }

    private IEnumerator SpeedBoostCoroutine()
    {
        Debug.Log("���� ����!");

        // ��� �ӵ� ���� & ���� ���� Ȱ��ȭ
        background.moveSpeed *= dashMultiplier;
        isInvincible = true;

        yield return new WaitForSeconds(dashDuration); // ���� �ð� ���� ����

        // ��� �ӵ� ���� ���� & ���� ���� ����
        background.moveSpeed = originalBGSpeed;
        isInvincible = false;

        Debug.Log("���� ����");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���� ���¿��� ��ֹ��� �浹�ϸ� �ı�
        if (isInvincible && other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject); // ��ֹ� ����
            //ScoreManager.Instance.AddScore(100); // ���� �߰�
            //Debug.Log("��ֹ� �ı�! +100��");

        }
    }
}
