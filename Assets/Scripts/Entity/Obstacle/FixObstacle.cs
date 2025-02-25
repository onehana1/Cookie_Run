using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixObstacle : BaseObstacle
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.TryGetComponent<BaseState>(out baseState);
        if (baseState == null) return; 
        this.OnDamage();
    }
}
