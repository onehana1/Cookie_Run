using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGalloping : MonoBehaviour
{
    private BackGroundController background; // ��� �ӵ� ��Ʈ�ѷ�
    private Invincibility invincibility; // ���� ���� ���� Ŭ����
    private BaseState baseState; // ü�� �� ĳ���� ���� ���� Ŭ����
    private float originalBGSpeed; // ���� ��� �ӵ� ����

    public float dashMultiplier = 2f; // ���� �� ��� �ӵ� ���� (�⺻ 2��)
    public float dashDuration = 7f; // ���� ���� �ð� (��)

    private void Start()
    {
        // �ش� ������Ʈ���� �ʿ��� ������Ʈ���� ������
        background = FindObjectOfType<BackGroundController>(); // ��� �ӵ� ����
        invincibility = GetComponent<Invincibility>(); // ���� ����
        baseState = GetComponent<BaseState>(); // ĳ���� ���� ����

        // ���� ��� �ӵ� ����
        originalBGSpeed = background.moveSpeed;
    }

    // ����(���) ���� �Լ�
    public void ActivateSpeedBoost()
    {
        StartCoroutine(SpeedBoostCoroutine());
    }

    // ���� ���� ȿ�� �ڷ�ƾ
    private IEnumerator SpeedBoostCoroutine()
    {
        Debug.Log("���� ����!");

        // ��� �ӵ� ���� �� ���� ���� Ȱ��ȭ
        background.moveSpeed *= dashMultiplier;
        invincibility.StartInvincibility(dashDuration); // ���� �ð� ���� ����

        // ���� ���� �ð� ���� ���
        yield return new WaitForSeconds(dashDuration);

        // ��� �ӵ��� ������� ����
        background.moveSpeed = originalBGSpeed;

        Debug.Log("���� ����");
    }

    // ���� ���¿��� ��ֹ� �ı�
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���� ������ ���� ��ֹ� �ı�
        if (invincibility.IsInvincible && other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
            
        }
    }

    // �浹 ���� 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���� ������ ��� �ǰ� ���� ����
        if (invincibility.IsInvincible)
        {
            Debug.Log("���� �����̹Ƿ� �������� ���� ����");
            return;
        }
    }
}
