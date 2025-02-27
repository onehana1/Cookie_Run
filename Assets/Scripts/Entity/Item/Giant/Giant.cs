using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : MonoBehaviour
{
    public float duration = 20f; // �Ŵ�ȭ ���� �ð�

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾ �������� ������
        {
            BaseController playerController = other.GetComponent<BaseController>();

            if (playerController != null)
            {
                playerController.SendMessage("SetBigger", duration); // �Ŵ�ȭ

                // GiantMode ������Ʈ �߰� �� Ȱ��ȭ
                GiantMode giantMode = playerController.gameObject.AddComponent<GiantMode>();
                giantMode.ActivateGiantMode(playerController, duration);
            }

            Destroy(gameObject); // ������ ��� �� ����
        }
    }
}
