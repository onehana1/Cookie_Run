using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_JellyController : MonoBehaviour
{
    public int scoreValue; // ������ ������ ��� ����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �浹�� ��ü�� Player���� Ȯ��
        {
            //GameManager.Instance.AddScore(scoreValue); // ���� �߰�
            Destroy(gameObject); // ���� ����
        }
    }
}
