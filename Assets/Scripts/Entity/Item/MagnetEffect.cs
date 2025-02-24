using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetEffect : MonoBehaviour
{
    private float range; // �ڼ� ����
    private float speed; // �������� �ӵ�
    private bool isMagnetActive = false; // �ڼ� ȿ�� ���� ����

    public void EnableMagnet(float magnetRange, float attractionSpeed)
    {
        range = magnetRange; // �ڼ� ���� ����
        speed = attractionSpeed; // �������� �ӵ� ����
        isMagnetActive = true; // �ڼ� Ȱ��ȭ
    }
    public void DisableMagnet()
    {
        isMagnetActive = false; // �ڼ� ȿ�� ��Ȱ��ȭ
    }
    private void Update()
    {
        if (!isMagnetActive) return; // �ڼ��� Ȱ��ȭ���� �ʾҴٸ� �ƹ��͵� �� ��

        Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D item in items)
        {
            if (item.CompareTag("Item")) // ������ �±װ� �ִ� ���
            {
                // �������� �÷��̾� �������� �̵�
                item.transform.position = Vector2.Lerp(item.transform.position, transform.position, speed * Time.deltaTime);
            }
        }
    }
}
