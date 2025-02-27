using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PlayerGalloping : MonoBehaviour
{
    // ��� �� �÷��̾� ���� ���� ����
    private BackGroundController background;
    private BaseState baseState;
    private PlayManager playManager;

    // ���� �ӵ��� ������ ����
    private float originalBGSpeed;
    private float originalMoveSpeed;
    private bool isBoosting = false; // ���� ������ ����

    public float dashMultiplier = 2f; // ���� �� �ӵ� ����
    public float dashDuration = 5f; // ���� ���� �ð�

    private void Start()
    {
        // �ʿ��� ������Ʈ �� �Ŵ��� ���� ��������
        background = FindObjectOfType<BackGroundController>();
        baseState = GetComponent<BaseState>();
        playManager = PlayManager.Instance;

        // ���� �ӵ� ����
        originalBGSpeed = background.moveSpeed;
        originalMoveSpeed = baseState.moveSpeed;
    }

    // ����Tiem ��� Ȱ��ȭ
    public void ActivateSpeedBoost(float duration, float multiplier)
    {
        if (isBoosting) return; // �̹� ���� ���̸� ���� �� ��
        StartCoroutine(SpeedBoostCoroutine(duration, multiplier));
    }

    // ����Tiem �ڷ�ƾ ����
    private IEnumerator SpeedBoostCoroutine(float duration, float multiplier)
    {
        isBoosting = true;
        Debug.Log("���� ����!");

        // ��� �� �÷��̾� �ӵ� ����
        background.moveSpeed *= multiplier;
        baseState.SetMoveSpeed(baseState.moveSpeed * multiplier);

        // ���� ���� Ȱ��ȭ
        baseState.StartInvincibility(duration);
        StartCoroutine(BlinkEffect(duration)); // �����̴� ȿ�� �߰�

        yield return new WaitForSeconds(duration);

        // ���� �ӵ��� ����
        background.moveSpeed = originalBGSpeed;
        baseState.SetMoveSpeed(originalMoveSpeed);

        isBoosting = false;
        Debug.Log("���� ����");
    }

    // ��ֹ��� �浹���� �� �ı��ϴ� ��smd
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isBoosting && other.CompareTag("Obstacle"))
        {
            SoundMananger.instance.PlayDestroyEffect();
            Destroy(other.gameObject);
            Debug.Log("��ֹ� �ı�!");
        }
    }

    // ��ֹ��� �浹���� �� �ı��ϴ� ���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBoosting && collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
            Debug.Log("�浹�� ��ֹ� �ı�!");
        }
    }

    // ���� ������ �� �����̴� ȿ�� �߰�
    private IEnumerator BlinkEffect(float duration)
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        float time = 0f;
        float blinkIntervalTime = 0.2f; // �����̴� ���� ����

        while (time < duration)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f); // ������ ����
            yield return new WaitForSeconds(blinkIntervalTime);
            spriteRenderer.color = new Color(1, 1, 1, 1.0f); // ���� ����
            yield return new WaitForSeconds(blinkIntervalTime);
            time += blinkIntervalTime * 2;
        }

        spriteRenderer.color = new Color(1, 1, 1, 1); // ���� ���� ����
    }
}