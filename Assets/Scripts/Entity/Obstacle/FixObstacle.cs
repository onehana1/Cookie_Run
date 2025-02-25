using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixObstacle : BaseObstacle
{
    public Animator animator;
    public bool isMove;

    private void Awake()
    {
        isMove = false;
    }

    private void FixedUpdate()
    {
        if (animator != null && !isMove && transform.position.x < 0)
        {
            isMove = true;
            animator.SetTrigger("Move");
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.TryGetComponent<BaseState>(out baseState);
        if (baseState == null) return; 
        this.OnDamage();
    }
}
