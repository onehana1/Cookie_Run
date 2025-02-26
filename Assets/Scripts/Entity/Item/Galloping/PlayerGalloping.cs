using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System.Collections;

public class PlayerGalloping : MonoBehaviour
{
    private BackGroundController background;
    private BaseState baseState;
    private PlayManager playManager;

    private float originalBGSpeed;
    private float originalMoveSpeed;
    private bool isBoosting = false;

    public float dashMultiplier = 2f;
    public float dashDuration = 5f;

    private void Start()
    {
        background = FindObjectOfType<BackGroundController>();
        baseState = GetComponent<BaseState>();
        playManager = PlayManager.Instance;

        originalBGSpeed = background.moveSpeed;
        originalMoveSpeed = baseState.moveSpeed;
    }

    public void ActivateSpeedBoost(float duration, float multiplier)
    {
        if (isBoosting) return;
        StartCoroutine(SpeedBoostCoroutine(duration, multiplier));
    }

    private IEnumerator SpeedBoostCoroutine(float duration, float multiplier)
    {
        isBoosting = true;
        Debug.Log("���� ����!");

        // ��� �ӵ� �� �̵� �ӵ� ����, HP ���� ����
        background.moveSpeed *= multiplier;
        baseState.SetMoveSpeed(baseState.moveSpeed * multiplier);
        float originalHp = playManager.hp;

        yield return new WaitForSeconds(duration);

        // ���� �ӵ��� ���� 
        background.moveSpeed = originalBGSpeed;
        baseState.SetMoveSpeed(originalMoveSpeed);
        playManager.hp = originalHp; // HP�� ���� ������ ����

        isBoosting = false;
        Debug.Log("���� ����");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isBoosting && other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
            Debug.Log("��ֹ� �ı�!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBoosting && collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
            Debug.Log("�浹�� ��ֹ� �ı�!");
        }
    }
}