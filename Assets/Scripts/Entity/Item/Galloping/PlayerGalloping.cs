using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGalloping : MonoBehaviour
{
    private BaseState baseState; // �÷��̾��� �̵� �ӵ��� �����ϴ� BaseState
    private BackGroundController background; // ��� �ӵ��� �����ϴ� ��Ʈ�ѷ�
    private bool isInvincible = false; // ���� ���� ���� (���� �� ����)

    private void Start()
    {
        // BaseState �������� (�÷��̾� �̵� �ӵ��� ����)
        baseState = GetComponent<BaseState>();

        // ������ BackGroundController ã�� (��� �ӵ��� �����ϱ� ����)
        background = FindObjectOfType<BackGroundController>();
    }

    /// <summary>
    /// ���� ��� Ȱ��ȭ: ���� �ð� ���� �ӵ��� ������Ű�� ���� ���� ����
    /// </summary>
    /// <param name="duration">���� ���� �ð�</param>
    /// <param name="multiplier">�ӵ� ���� ����</param>
    public IEnumerator ActivateSpeedBoost(float duration, float multiplier)
    {
        Debug.Log("���� ����!");

        
        float originalSpeed = baseState.moveSpeed; // ���� �ӵ� ���� (���� ������ ���� �ӵ� ����)
        float originalBGSpeed = background.moveSpeed; // ���� ��� �ӵ� ����

        // �̵� �ӵ� �� ��� �ӵ� ����
        baseState.moveSpeed *= multiplier;
        background.moveSpeed *= multiplier;
        isInvincible = true; // ���� ���� Ȱ��ȭ

        // ������ �ð�(duration)��ŭ ���
        yield return new WaitForSeconds(duration);

        // �ӵ� �� ��� �ӵ��� ������� ����
        baseState.moveSpeed = originalSpeed;
        background.moveSpeed = originalBGSpeed;
        isInvincible = false; // ���� ���� ����

        Debug.Log("���� ����");
    }
}
