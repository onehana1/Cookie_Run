using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapCreater : MonoBehaviour
{
    public GameObject[] mapPrefabs;//맵 프리팹
    public Queue <GameObject> maps;//생성된 맵들
    public GameObject endMap;
    Transform firstEndAnchor = null;//첫번째 맵의 끝나는 지점
    Transform lastEndAnchor = null;//마지막 맵의 끝나는 지점
    
    void Start()
    {
        GameObject go = transform.Find("Grounds").gameObject;
        if (go == null)
        {
            Debug.LogError("Can not find Grounds");
        }
        maps = new Queue <GameObject>();
        maps.Enqueue(go);
        lastEndAnchor = firstEndAnchor = maps.Peek().GetComponent<MapController>().endAnchor;//기본적으로 깔려있는 땅의 값으로 초기화
        CreateMap();//맵생성
    }

    private void FixedUpdate()
    {
        if(firstEndAnchor.transform.position.x <= -10)//맵 삭제
        {
            GameObject go = maps.Peek();
            maps.Dequeue();
            Destroy(go);
            firstEndAnchor = maps.Peek().GetComponent<MapController>().endAnchor;
        }

        if(lastEndAnchor.transform.position.x <= 15)//맵 \생성
        {
            if (!PlayManager.Instance.isEnd)
            {
                CreateMap();
            }
            else
            {
                CreateEndMap();
            }
        }
    }

    private void CreateMap()
    {
        //랜덤한 맵 생성
        int randomMapNumber = Random.Range(0, mapPrefabs.Length);
        GameObject go = Instantiate(mapPrefabs[randomMapNumber], this.transform);

        go.transform.position = lastEndAnchor.position;
        lastEndAnchor = go.GetComponent<MapController>().endAnchor;
        maps.Enqueue(go);
    }

    private void CreateEndMap()//마지막 맵 생성
    {
        GameObject go = Instantiate(endMap, this.transform);
        go.transform.position = lastEndAnchor.position;
        lastEndAnchor = go.GetComponent<MapController>().endAnchor;
        maps.Enqueue(go);
    }
}
