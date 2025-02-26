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
    public bool isDead = false;
    public bool isRescue = false;
    public bool isRand = false;
    public bool isFall = false;

    [Header("Test")]
    [SerializeField] public bool isInvincible = false;


    [Header("Health Decay")]
    [SerializeField] private float healthDecayRate = 1.0f; // 초당 감소할 체력량
    [SerializeField] private float healthDecayInterval = 5.0f; // 몇 초마다 감소할 것인지


    public enum HPReduceType { Damage, Normal }

    public event Action<float, float> OnTakeDamage;
    public event Action<float, float> OnHpChanged;


    public event Action OnDie;

    private void Awake()
    {
        hp = maxHp;
    }
    private void Start()
    {
        StartCoroutine(HealthDecayRoutine());
    }

    public void TakeDamage(float damage, HPReduceType hPReduceType = HPReduceType.Damage)
    {
        if (!isLive) return;

        hp -= damage;
        Debug.Log($"현재 체력: {hp}");

        OnHpChanged?.Invoke(maxHp, hp);

        if (hPReduceType == HPReduceType.Damage)
        {
            OnTakeDamage?.Invoke(maxHp, hp);
        }

        if (hp <= 0)
        {
            hp = 0;
            isLive = false;
            Debug.Log("캐릭터 사망");
            OnDie?.Invoke();
        }
    }

    public void Heal(float amount)
    {
        if (!isLive) return;

        hp += amount;
        if (hp > maxHp) hp = maxHp; 

        OnTakeDamage?.Invoke(maxHp, hp); 
    }


    public void Die()
    {
        if (!isLive) return;

        isLive = false;
        isDead = true;  // 사망 상태 활성화

        Debug.Log("캐릭터 사망");
        OnDie?.Invoke();
    }

    private IEnumerator HealthDecayRoutine()
    {
        while (isLive)
        {
            yield return new WaitForSeconds(healthDecayInterval);
            TakeDamage(healthDecayRate, HPReduceType.Normal);
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
