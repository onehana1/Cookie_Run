using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHealPack : MonoBehaviour
{
    public float healAmount = 20f; // ȸ����

    private void OnTriggerEnter2D(Collider2D other) // �÷��̾ �浹�ϸ�
    {
        if (other.CompareTag("Player")) // �±� "Player"
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>(); // �÷��̾��� ü�� ��ũ��Ʈ ��������
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount); // ü�� ȸ��
            }

            Destroy(gameObject); // ���� ������ ����
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
