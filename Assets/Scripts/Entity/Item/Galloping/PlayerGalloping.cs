using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGalloping : MonoBehaviour
{
    private BackGroundController background;
    private Invincibility invincibility;
    private BaseState baseState; // ü�� �����ϴ� BaseState
    private float originalBGSpeed;

    public float dashMultiplier = 2f;
    public float dashDuration = 2f;

    private void Start()
    {
        background = FindObjectOfType<BackGroundController>();
        invincibility = GetComponent<Invincibility>();
        baseState = GetComponent<BaseState>();
        originalBGSpeed = background.moveSpeed;
    }

    public void ActivateSpeedBoost()
    {
        StartCoroutine(SpeedBoostCoroutine());
    }

    private IEnumerator SpeedBoostCoroutine()
    {
        Debug.Log("���� ����!");

        background.moveSpeed *= dashMultiplier;
        invincibility.StartInvincibility(dashDuration);

        yield return new WaitForSeconds(dashDuration);

        background.moveSpeed = originalBGSpeed;
        Debug.Log("���� ����");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���� ���¿��� ��ֹ��� �浹�ϸ� �ı�
        if (invincibility.IsInvincible && other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
            Debug.Log("��ֹ� �ı�!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���� ���¶�� �ǰ��� ����
        if (invincibility.IsInvincible)
        {
            Debug.Log("���� �����̹Ƿ� �������� ���� ����");
            return;
        }

        // ���̳� ��ֹ��� �浹 �� ������ ó��
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Obstacle"))
        {
            baseState.TakeDamage(10f); // 10 ������ ����
            Debug.Log("�������� ����!");
        }
    }
}
