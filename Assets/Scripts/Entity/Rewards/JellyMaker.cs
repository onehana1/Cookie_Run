using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class JellyMaker : MonoBehaviour
{
    //���� �θ������Ʈ
    public GameObject JellyObject; 

    //�����Ǵ� ���� ������ ����
    public GameObject jellyPrefab1;
    public GameObject jellyPrefab2;
    public GameObject jellyPrefab3;

    //�����Ǵ� ���� ������ ����
    public GameObject coinPrefab1;
    public GameObject coinPrefab2;
    public GameObject coinPrefab3;

    //������ ������Ʈ
    private GameObject jellyObj;
    private GameObject coinObj;

    //������ ��ġ�� ������Ʈ(��ġ ������)
    private GameObject preObj;

    //������ �����ؾ��ϴ� y��
    Vector2 pivot;

    //�׶����� �⺻ pivot��
    Vector2 groundVector = new Vector2(20f, -3f);

    //���̿� ���̸� ������ ���� posA
    Vector3 posA; 
    float length = 1f;

    //���� ���� ����? �װ�
    float t = 0.2f;

    //����/���� Ÿ��& ��� ����
    int type;
    int typeCount;


    private void Awake()
    {
        pivot = groundVector;
        posA = transform.position;

        type = Random.Range(0, 100);
        typeCount = Random.Range(5, 10);
    }

    private void Start()
    {
        if (type < 50)
        {
            MakeJelly(posA);
            return;
        }
        MakeCoin(posA);
    }

    private void Update()
    {
        if (typeCount == 0)
        {
            type = Random.Range(0, 100);
            typeCount = Random.Range(5, 10);
        }

        //���� �������� ������ �����ϱ�
        if ((preObj.transform.position - posA).magnitude >= length)
        {
            if (type < 50)
            {
                MakeJelly(posA);
                return;
            }
            MakeCoin(posA);
        }

        //����, ������ y�� ��ġ �����ϴ� ó��
        posA.y = Mathf.Lerp(posA.y, pivot.y, t);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pivot"))
        {
            if(collision.name == "EndPivot")
            {
                this.gameObject.SetActive(false);
            }
            coinObj = coinPrefab2;
            jellyObj = jellyPrefab2;
            pivot = collision.transform.position;
        }
    }

    //���� ��������
    private void MakeJelly(Vector2 pos)
    {
        switch (type)
        {
            case < 20:
                jellyObj = jellyPrefab3;
                break;
            case < 50:
                jellyObj = jellyPrefab2;
                break;
            default:
                jellyObj = jellyPrefab1;
                break;
        }

        preObj = Instantiate(jellyObj, pos, Quaternion.identity, JellyObject.transform);
        typeCount--;
    }

    //���� ��������
    private void MakeCoin(Vector2 pos)
    {
        switch (type)
        {
            case < 20:
                coinObj = coinPrefab3;
                break;
            case < 50:
                coinObj = coinPrefab2;
                break;
            default:
                coinObj = coinPrefab1;
                break;
        }

        preObj = Instantiate(coinObj, pos, Quaternion.identity, JellyObject.transform);
        typeCount--;
    }
}