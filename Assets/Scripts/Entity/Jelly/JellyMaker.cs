using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class JellyMaker : MonoBehaviour
{
    Queue<Collider2D> obstacleQueue = new Queue<Collider2D>();

    //젤리 부모오브젝트
    public GameObject JellyObject; 
    //생성되는 젤리 프리펩
    public GameObject jellyPrefab;
    //이전에 설치된 젤리 오브젝트
    private GameObject preJelly;

    //젤리가 도달해야하는 y값
    Vector2 pivot;

    //그라운드의 기본 pivot값
    Vector2 groundVector = new Vector2(20f, -3f);

    //길이를 구하는 벡터 posA가 메이커의 벡터
    Vector3 posA;

    float length = 1f;

    [SerializeField] float t = 0.2f;

    private void Awake()
    {
        pivot = groundVector;
        posA = transform.position;
        MakeJelly(posA);
    }

    ////1초에 50번 생성
    //private void FixedUpdate()
    //{
    //    //일정 시간간격으로 젤리를 생성함
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

    //    //피벗 값 받아서 러프 적용?
    //    position.y = Mathf.Lerp(position.y, pivot.y, 0.25f);
    //}

    private void Update()
    {
        //일정 간격으로 젤리를 생성하기
        if ((preJelly.transform.position - posA).magnitude >= length)
        {
            MakeJelly(posA);
        }

        //젤리의 y값 위치 수정
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