using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class JellyMaker : MonoBehaviour
{
    //젤리 부모오브젝트
    public GameObject JellyObject; 

    //생성되는 젤리 프리펩 종류
    public GameObject jellyPrefab1;
    public GameObject jellyPrefab2;
    public GameObject jellyPrefab3;

    //생성되는 코인 프리펩 종류
    public GameObject coinPrefab1;
    public GameObject coinPrefab2;
    public GameObject coinPrefab3;

    //생성할 오브젝트
    private GameObject jellyObj;
    private GameObject coinObj;

    //이전에 설치된 젤리 오브젝트
    private GameObject preObj;

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

        jellyObj = jellyPrefab1;
        coinObj = coinPrefab1;

        MakeJelly(posA);
    }



    private void Update()
    {
        //일정 간격으로 젤리를 생성하기
        if ((preObj.transform.position - posA).magnitude >= length)
        {

            if (Random.Range(0, 100) < 50)
            {
                MakeJelly(posA);
                return;
            }
            MakeCoin(posA);
        }

        //젤리, 코인의 y값 위치 수정하는 처리
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