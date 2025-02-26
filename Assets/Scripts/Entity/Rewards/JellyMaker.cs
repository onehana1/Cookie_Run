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

    //������ ��ġ�� ���� ������Ʈ
    private GameObject preObj;

    //������ �����ؾ��ϴ� y��
    Vector2 pivot;

    //�׶����� �⺻ pivot��
    Vector2 groundVector = new Vector2(20f, -3f);

    //���̸� ���ϴ� ���� posA�� ����Ŀ�� ����
    Vector3 posA;

    float length = 1f;

    [SerializeField] float t = 0.2f;

    private void Awake()
    {
        pivot = groundVector;
        posA = transform.position;

        jellyObj = jellyPrefab1;
        coinObj = coinPrefab1;

        MakeJelly(posA);
    }



    private void Update()
    {
        //���� �������� ������ �����ϱ�
        if ((preObj.transform.position - posA).magnitude >= length)
        {

            if (Random.Range(0, 100) < 50)
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
            coinObj = coinPrefab2;
            jellyObj = jellyPrefab2;
            pivot = collision.transform.position;
        }
    }

    private void MakeJelly(Vector2 pos)
    {
        //switch (Random.Range(0, 100))
        //{
        //    case < 20:
        //        jellyObj = jellyPrefab3;
        //        break;
        //    case < 50:
        //        jellyObj = jellyPrefab2;
        //        break;
        //    default:
        //        jellyObj = jellyPrefab1;
        //        break;
        //}

        preObj = Instantiate(jellyObj, pos, Quaternion.identity, JellyObject.transform);
    }

    private void MakeCoin(Vector2 pos)
    {
        //switch (Random.Range(0, 100))
        //{
        //    case < 20:
        //        coinObj = coinPrefab3;
        //        break;
        //    case < 50:
        //        coinObj = coinPrefab2;
        //        break;
        //    default:
        //        coinObj = coinPrefab1;
        //        break;
        //}

        preObj = Instantiate(coinObj, pos, Quaternion.identity, JellyObject.transform);
    }
}
////1�ʿ� 50�� ����
//private void FixedUpdate()
//{
//    //���� �ð��������� ������ ������
//    time += Time.fixedDeltaTime;
//    posB = transform.position;
//    if (time >= 0.2f)
//    {
//        time = 0;
//        MakeJelly(position);
//    }

//    if ((posB - posA).magnitude >= length)
//    {
//        MakeJelly(posB);
//        posA = posB;
//    }

//    //�ǹ� �� �޾Ƽ� ���� ����?
//    position.y = Mathf.Lerp(position.y, pivot.y, 0.25f);
//}