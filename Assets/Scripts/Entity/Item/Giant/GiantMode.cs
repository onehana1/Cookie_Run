using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantMode : MonoBehaviour
{
    private BaseController playerController;
    private Collider2D playerCollider;
    private bool isGiantActive = false;

    public void ActivateGiantMode(BaseController controller, float duration)
    {
        if (isGiantActive) return; // �̹� Ȱ��ȭ�� ��� �ߺ� ���� ����

        isGiantActive = true;
        playerController = controller;
        playerCollider = controller.GetComponent<Collider2D>();

        // ���� ���� Ȱ��ȭ
        playerController.baseState.StartInvincibility(duration);

        // ��ֹ� �ı� ��� Ȱ��ȭ
        // playerCollider.isTrigger = true;

        // ���� �ð� �� ���� ���·� ����
        StartCoroutine(DisableGiantMode(duration));
    }

    private IEnumerator DisableGiantMode(float duration)
    {
        yield return new WaitForSeconds(duration);

        // ���� ���·� ����
        // playerCollider.isTrigger = false;
        isGiantActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isGiantActive && other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject); // ��ֹ� �ı�
        }
    }
}