using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galloping : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾ �������� ������
        {
            PlayerGalloping playerGalloping = other.GetComponent<PlayerGalloping>();
            if (playerGalloping != null)
            {
                playerGalloping.ActivateSpeedBoost(); // ���� ����
            }

            Destroy(gameObject); // ������ ����

        }
    }
}
