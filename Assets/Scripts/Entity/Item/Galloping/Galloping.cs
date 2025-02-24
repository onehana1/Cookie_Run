using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galloping : MonoBehaviour
{
    public float boostDuration = 2.0f; // ���� ���� �ð�
    public float speedMultiplier = 2.5f; // �ӵ� ���� ����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾ �������� ������
        {
            PlayerGalloping player = other.GetComponent<PlayerGalloping>();
            if (player != null)
            {
                player.StartCoroutine(player.ActivateSpeedBoost(boostDuration, speedMultiplier)); // ���� ȿ�� Ȱ��ȭ
            }

            Destroy(gameObject); // ������ ����
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
