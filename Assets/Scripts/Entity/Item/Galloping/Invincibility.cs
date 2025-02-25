using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isInvincible = false; // ���� ���� ����

    public bool IsInvincible => isInvincible; // �ܺο��� ���� ���� Ȯ�� ����

    // ���� ���¸� �����ϴ� �޼ҵ�
    public void StartInvincibility(float duration)
    {
        if (isInvincible) return; // �̹� ���� ���¶�� �ߺ� ���� ����
        StartCoroutine(InvincibilityCoroutine(duration));
    }

    private IEnumerator InvincibilityCoroutine(float duration)
    {
        isInvincible = true; // ���� Ȱ��ȭ
        Debug.Log("���� ���� ON");

        yield return new WaitForSeconds(duration); // ���� �ð� ���� ����

        isInvincible = false; // ���� ����
        Debug.Log("���� ���� OFF");
    }
}
