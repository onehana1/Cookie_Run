using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : MonoBehaviour
{
    public float duration = 5f; // �Ŵ�ȭ ���� �ð�

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾ �������� ������
        {
            BaseController playerController = other.GetComponent<BaseController>();

            if (playerController != null)
            {
                playerController.SendMessage("SetBigger", duration);
            }

            Destroy(gameObject); // ������ ��� �� ����
        }
    }
}
