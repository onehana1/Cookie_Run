using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseObstacle : MonoBehaviour
{
    protected int Damage;
    protected void OnDamage(Collider2D collider2D)
    {
        //캐릭터 피를 깐다는 메서드 작성해야 함.
        //if(collider2D.FindObjectOfType</*플레이어 스크립트*/>)
        {
            //collider2D.GetComponent</*플레이어 스크립트*/> 데미지값 -= Damage;
        }
    }
}