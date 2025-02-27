using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NHealPack : MonoBehaviour
{
    public float healAmount = 50f; // ȸ����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾�� �浹���� ��
        {
            SoundMananger.instance.PlayHealEffect();
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
