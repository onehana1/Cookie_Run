using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseObstacle : MonoBehaviour
{
    BaseState baseState;

    float damage = 10f;
    protected void OnDamage()
    {
        baseState.TakeDamage(damage);
    }
}