using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCoin : MonoBehaviour
{
    public int coin;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            PlayManager.Instance.AddCoin(coin);
        }

        else if (collision.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
