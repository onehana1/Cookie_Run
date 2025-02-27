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
            SoundMananger.instance.PlayJellyEffect();
            Destroy(gameObject); // ���� ����
            PlayManager.Instance.AddScore(scoreValue); // ���� �߰�
        }

        else if (other.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
