using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCoin : MonoBehaviour
{
    public int coin;
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_animator == null) return;
        _animator.SetFloat("SpinSpeed", 1/Time.timeScale);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundMananger.instance.PlayCoinEffect();
            Destroy(gameObject);
            PlayManager.Instance.AddCoin(coin);
        }

        else if (collision.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}