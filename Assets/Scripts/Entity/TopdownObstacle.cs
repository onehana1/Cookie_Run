using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopdownObstacle : BaseObstacle
{
    protected Animator animator;
    
    public int damage;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("½ÇÇà");
        animator.enabled = true;
        base.OnDamage();
    }
}