using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseObstacle : MonoBehaviour
{
    protected int Damage;
    protected void OnDamage(Collider2D collider2D)
    {
        //ĳ���� �Ǹ� ��ٴ� �޼��� �ۼ��ؾ� ��.
        //if(collider2D.FindObjectOfType</*�÷��̾� ��ũ��Ʈ*/>)
        {
            //collider2D.GetComponent</*�÷��̾� ��ũ��Ʈ*/> �������� -= Damage;
        }
    }
}