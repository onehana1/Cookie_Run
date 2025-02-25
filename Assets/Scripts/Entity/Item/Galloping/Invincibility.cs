using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Invincibility : MonoBehaviour
{
 
    private bool isInvincible = false; // ���� ���� ����
    public bool IsInvincible => isInvincible; // �ܺο��� Ȯ�� ����

    public void StartInvincibility(float duration)
    {
        if (isInvincible) return; // �ߺ� ���� ����
        StartCoroutine(InvincibilityCoroutine(duration));
    }

    private IEnumerator InvincibilityCoroutine(float duration)
    {
        isInvincible = true;
        Debug.Log("���� ���� ON");

        yield return new WaitForSeconds(duration);

        isInvincible = false;
        Debug.Log("���� ���� OFF");
    }
}