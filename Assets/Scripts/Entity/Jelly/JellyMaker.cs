using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class JellyMaker : MonoBehaviour
{
    Vector2 pivot;
    Vector2 position;

    //길이1번 길이 2번
    Vector2 posA;
    Vector2 posB;
    Queue<Collider2D> obstacleQueue = new Queue<Collider2D>();

    float length = 1;

    public GameObject jelly;
    public GameObject JellyObject;

    private void Awake()
    {
        pivot = new Vector2(20f, -3f);
        position = transform.position;
        posA = transform.position;
    }

    //컴퓨터 성능에 따라 생성
    private void Update()
    {
        position.x += Time.deltaTime;

        transform.position = position;
    }

    ////1초에 50번 생성
    //private void FixedUpdate()
    //{
    //    //일정 시간간격으로 젤리를 생성함
    //    time += Time.fixedDeltaTime;

    //    if (time >= 1)
    //    {
    //        time = 0;
    //        MakeJelly(position);
    //    }

    //    //피벗 값 받아서 러프 적용?
    //    position.y = Mathf.Lerp(position.y, pivot.y, 0.25f);
    //}

    //1초에 50번 생성
    private void FixedUpdate()
    {
        posB = transform.position;

        
        if ((posB - posA).magnitude >= length)
        {
            MakeJelly(posB);
            posA =posB;
        }
        
        position.y = Mathf.Lerp(position.y, pivot.y, 0.25f);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        pivot = collision.transform.Find("Pivot").transform.position;
        obstacleQueue.Enqueue(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        obstacleQueue.Dequeue();
        if (obstacleQueue.Count == 0) pivot.y = -3f;
    }

    private void MakeJelly(Vector2 pos)
    {
        Instantiate(jelly, pos, Quaternion.identity, JellyObject.transform);
    }
}
