using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseJelly : MonoBehaviour
{
    public int scoreValue; // ������ ������ ��� ����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �浹�� ��ü�� Player���� Ȯ��
        {
            GameManager.Instance.AddScore(scoreValue); // ���� �߰�
            Destroy(gameObject); // ���� ����
        }

        else if (other.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
