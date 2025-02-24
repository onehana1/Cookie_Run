using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapCreater : MonoBehaviour
{
    public GameObject[] mapPrefabs;//맵 프리팹
    public List <GameObject> maps;//생성된 맵들
    Transform firstEndAnchor = null;//첫번째 맵의 끝나는 지점
    Transform lastEndAnchor = null;//마지막 맵의 끝나는 지점
    
    // Start is called before the first frame update
    void Start()
    {
        lastEndAnchor = firstEndAnchor = maps[0].GetComponent<MapController>().endAnchor;//기본적으로 깔려있는 땅의 값으로 초기화
        CreateMap();//맵생성
    }

    private void FixedUpdate()
    {
        if(firstEndAnchor.transform.position.x <= -10)//맵 삭제
        {
            Destroy(maps[0]);
            maps.Remove(maps[0]);
            firstEndAnchor = maps[0].GetComponent<MapController>().endAnchor;
        }

        if(lastEndAnchor.transform.position.x <= 15)//맵 \생성
        {
            CreateMap();       
        }
    }

    private void CreateMap()
    {
        //랜덤한 맵 생성
        int randomMapNumber = Random.Range(0, mapPrefabs.Length);
        GameObject go = Instantiate(mapPrefabs[randomMapNumber], this.transform);

        go.transform.position = lastEndAnchor.position;
        lastEndAnchor = go.GetComponent<MapController>().endAnchor;
        maps.Add(go);
    }
}
