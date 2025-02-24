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
    //�����Ǵ� ���� ������
    public GameObject jellyPrefab;
    //������ ��ġ�� ���� ������Ʈ
    private GameObject preJelly;

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
        MakeJelly(posA);
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

    private void Update()
    {
        //���� �������� ������ �����ϱ�
        if ((preJelly.transform.position - posA).magnitude >= length)
        {
            MakeJelly(posA);
        }

        //������ y�� ��ġ ����
        posA.y = Mathf.Lerp(posA.y, pivot.y, t);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            pivot = collision.transform.Find("Pivot").transform.position;
            obstacleQueue.Enqueue(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            obstacleQueue.Dequeue();
            if (obstacleQueue.Count == 0) pivot.y = -3f;
        }
    }

    private void MakeJelly(Vector2 pos)
    {
        preJelly = Instantiate(jellyPrefab, pos, Quaternion.identity, JellyObject.transform);
    }
}