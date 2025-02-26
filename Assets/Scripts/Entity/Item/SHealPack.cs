using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHealPack : MonoBehaviour
{
    public float healAmount = 20f; // ȸ����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾�� �浹���� ��
        {
            BaseState baseState = other.GetComponent<BaseState>(); // �÷��̾��� BaseState ��������

            if (baseState != null)
            {
                baseState.Heal(healAmount); // ü�� ȸ��
                Debug.Log("ü�� ȸ��: " + healAmount);
            }

            Destroy(gameObject); // ������ ����
        }
    }

}
