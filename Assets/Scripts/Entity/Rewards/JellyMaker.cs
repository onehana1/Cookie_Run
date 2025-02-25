using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class JellyMaker : MonoBehaviour
{
    Queue<Collider2D> obstacleQueue = new Queue<Collider2D>();

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

            if (Random.Range(0, 100) < 30)
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
            obstacleQueue.Enqueue(collision);
            coinObj = coinPrefab2;
            jellyObj = jellyPrefab2;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Platform"))
        {
            obstacleQueue.Dequeue();
            if (obstacleQueue.Count == 0) pivot.y = -3f;

            jellyObj = jellyPrefab3;
            coinObj = coinPrefab3;
        }
    }

    private void MakeJelly(Vector2 pos)
    {
        preObj = Instantiate(jellyObj, pos, Quaternion.identity, JellyObject.transform);
    }

    private void MakeCoin(Vector2 pos)
    {
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