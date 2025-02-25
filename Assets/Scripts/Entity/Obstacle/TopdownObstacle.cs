using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopdownObstacle : BaseObstacle
{
    protected Animator animator;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("¾Æ¾ß!");
        baseState = collision.gameObject.GetComponent<BaseState>();
        animator.enabled = true;
        base.OnDamage();
    }
}