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
            SoundMananger.instance.PlayGiantEffect();
            BaseController playerController = other.GetComponent<BaseController>();
            
            if (playerController != null)
            {
                // SetBigger �޼��尡 �����ϴ� ��� ���� ȣ��
                playerController.SetBigger(duration); // �Ŵ�ȭ

                // �̹� GiantMode�� Ȱ��ȭ�Ǿ����� Ȯ���ϰ�, ���ٸ� Ȱ��ȭ
                GiantMode giantMode = playerController.GetComponent<GiantMode>();
                if (giantMode == null)
                {
                    giantMode = playerController.gameObject.AddComponent<GiantMode>();
                    giantMode.ActivateGiantMode(playerController, duration);
                }
            }

            Destroy(gameObject); // ������ ��� �� ����
        }
    }
}
