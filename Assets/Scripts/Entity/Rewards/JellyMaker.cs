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

    //이전에 설치된 오브젝트(위치 포착용)
    private GameObject preObj;

    //젤리가 도달해야하는 y값
    Vector2 pivot;

    //그라운드의 기본 pivot값
    Vector2 groundVector = new Vector2(20f, -3f);

    //길이와 길이를 측정할 벡터 posA
    Vector3 posA; 
    float length = 1f;

    //러프 보간 비율? 그것
    float t = 0.2f;

    //젤리/코인 타입& 출력 갯수
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

        //일정 간격으로 젤리를 생성하기
        if ((preObj.transform.position - posA).magnitude >= length)
        {
            if (type < 50)
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
            if(collision.name == "EndPivot")
            {
                this.gameObject.SetActive(false);
            }
            coinObj = coinPrefab2;
            jellyObj = jellyPrefab2;
            pivot = collision.transform.position;
        }
    }

    //젤리 생성로직
    private void MakeJelly(Vector2 pos)
    {
        switch (type)
        {
            case < 20:
                jellyObj = jellyPrefab3;
                break;
            case < 40:
                jellyObj = jellyPrefab2;
                break;
            default:
                jellyObj = jellyPrefab1;
                break;
        }

        preObj = Instantiate(jellyObj, pos, Quaternion.identity, JellyObject.transform);
        typeCount--;
    }

    //코인 생성로직
    private void MakeCoin(Vector2 pos)
    {
        switch (type)
        {
            case > 80:
                coinObj = coinPrefab3;
                break;
            case > 60:
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