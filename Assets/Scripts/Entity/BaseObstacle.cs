using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseObstacle : MonoBehaviour
{
    protected void OnDamage()
    {
        //ĳ���� �Ǹ� ��ٴ� �޼��� �ۼ��ؾ� ��.
        //if(collider2D.FindObjectOfType</*�÷��̾� ��ũ��Ʈ*/>)
        {
            //collider2D.GetComponent</*�÷��̾� ��ũ��Ʈ*/> �������� -= Damage;
        }
    }
}