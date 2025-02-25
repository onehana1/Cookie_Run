using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galloping : MonoBehaviour
{
    public float boostDuration = 5f; // ���� ���� �ð�
    public float speedMultiplier = 2f; // �̵� �ӵ� ����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerGalloping playerGalloping = other.GetComponent<PlayerGalloping>();
            if (playerGalloping != null)
            {
                playerGalloping.ActivateSpeedBoost(boostDuration, speedMultiplier);
            }
            Destroy(gameObject); // ������ ��� �� ����
        }
    }
}
