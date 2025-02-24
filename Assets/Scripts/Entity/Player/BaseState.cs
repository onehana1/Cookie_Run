using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public float maxHp = 100f;
    [SerializeField] public float hp;
    [SerializeField] public float moveSpeed = 10.0f;
    [SerializeField] public float jumpForce = 10.0f;

    public bool isFastRunning = false;
    public bool isBigger = false;

    public bool isGrounded = true;
    public bool isJump = false;
    public bool isDoubleJump = false;
    public bool isSliding = false;
    public bool isHit = false;
    public bool isLive = true;
    public bool isRescue = false;

    [Header("Test")]
    [SerializeField] public bool isInvincible = false;



    public event Action<float> OnTakeDamage;
    public event Action OnDie;

    private void Awake()
    {
        hp = maxHp;
    }
    public void TakeDamage(float damage)
    {
        if (isInvincible || !isLive) return;

        hp -= damage;
        Debug.Log($"���� ü��: {hp}");

        OnTakeDamage?.Invoke(hp);

        if (hp <= 0)
        {
            hp = 0;
            isLive = false;
            Debug.Log("ĳ���� ���");
            OnDie?.Invoke();
        }
    }

    public void StartInvincibility(float duration)
    {
        if (isInvincible) return;
        isInvincible = true;
        StartCoroutine(InvincibilityTimer(duration));
    }

    private IEnumerator InvincibilityTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }



    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }


}
