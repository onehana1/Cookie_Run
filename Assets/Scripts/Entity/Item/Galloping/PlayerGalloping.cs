using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System.Collections;

public class PlayerGalloping : MonoBehaviour
{
    private BackGroundController background;
    private Invincibility invincibility;
    private BaseState baseState;

    private float originalBGSpeed; // ���� ��� �ӵ� ����
    private float originalMoveSpeed; // ���� �̵� �ӵ� ����
    private bool isBoosting = false;

    public float dashMultiplier = 2f; // �⺻ �ӵ� ����
    public float dashDuration = 5f; // �⺻ ���� �ð�

    private void Start()
    {
        background = FindObjectOfType<BackGroundController>();
        invincibility = GetComponent<Invincibility>();
        baseState = GetComponent<BaseState>();

        originalBGSpeed = background.moveSpeed;
        originalMoveSpeed = baseState.moveSpeed;
    }

    public void ActivateSpeedBoost(float duration, float multiplier)
    {
        if (isBoosting) return; // �̹� Ȱ��ȭ�� ��� �ߺ� ���� ����
        StartCoroutine(SpeedBoostCoroutine(duration, multiplier));
    }

    private IEnumerator SpeedBoostCoroutine(float duration, float multiplier)
    {
        isBoosting = true;
        Debug.Log("���� ����!");

        // ��� �ӵ� �� �̵� �ӵ� ����, ���� ���� Ȱ��ȭ
        background.moveSpeed *= multiplier;
        baseState.SetMoveSpeed(baseState.moveSpeed * multiplier);
        invincibility.StartInvincibility(duration);

        yield return new WaitForSeconds(duration);

        // ���� �ӵ��� ����
        background.moveSpeed = originalBGSpeed;
        baseState.SetMoveSpeed(originalMoveSpeed);

        isBoosting = false;
        Debug.Log("���� ����");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (invincibility.IsInvincible && other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
            Debug.Log("��ֹ� �ı�!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (invincibility.IsInvincible && collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
            Debug.Log("�浹�� ��ֹ� �ı�!");
        }
        else if (!invincibility.IsInvincible &&
                 (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Obstacle")))
        {
            baseState.TakeDamage(10f);
            Debug.Log("�������� ����!");
        }
    }
}