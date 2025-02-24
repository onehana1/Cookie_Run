using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGalloping : MonoBehaviour
{
    private BaseState baseState;
    private BackGroundController background; // ��� ��Ʈ�ѷ� ��������
    private bool isInvincible = false;

    private void Start()
    {
        baseState = GetComponent<BaseState>();
        background = FindObjectOfType<BackGroundController>(); // ��� �ӵ� ��Ʈ��
    }

    public IEnumerator ActivateSpeedBoost(float duration, float multiplier)
    {
        Debug.Log("���� ����!");

        float originalSpeed = baseState.moveSpeed;
        float originalBGSpeed = background.moveSpeed; // ���� ��� �ӵ� ����

        baseState.moveSpeed *= multiplier;
        background.moveSpeed *= multiplier; // ��� �ӵ� ����
        isInvincible = true;

        yield return new WaitForSeconds(duration);

        baseState.moveSpeed = originalSpeed;
        background.moveSpeed = originalBGSpeed; // ��� �ӵ� ����
        isInvincible = false;

        Debug.Log("���� ����");
    }
}
