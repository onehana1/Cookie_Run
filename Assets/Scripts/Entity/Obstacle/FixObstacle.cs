using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixObstacle : BaseObstacle
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        baseState = collision.gameObject.GetComponent<BaseState>();
        base.OnDamage();
    }
}
