using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapCreater : MonoBehaviour
{
    public GameObject[] mapPrefabs;//�� ������
    public List <GameObject> maps;//������ �ʵ�
    Transform firstEndAnchor = null;//ù��° ���� ������ ����
    Transform lastEndAnchor = null;//������ ���� ������ ����
    
    // Start is called before the first frame update
    void Start()
    {
        lastEndAnchor = firstEndAnchor = maps[0].GetComponent<MapController>().endAnchor;//�⺻������ ����ִ� ���� ������ �ʱ�ȭ
        CreateMap();//�ʻ���
    }

    private void FixedUpdate()
    {
        if(firstEndAnchor.transform.position.x <= -10)//�� ����
        {
            Destroy(maps[0]);
            maps.Remove(maps[0]);
            firstEndAnchor = maps[0].GetComponent<MapController>().endAnchor;
        }

        if(lastEndAnchor.transform.position.x <= 15)//�� \����
        {
            CreateMap();       
        }
    }

    private void CreateMap()
    {
        //������ �� ����
        int randomMapNumber = Random.Range(0, mapPrefabs.Length);
        GameObject go = Instantiate(mapPrefabs[randomMapNumber], this.transform);

        go.transform.position = lastEndAnchor.position;
        lastEndAnchor = go.GetComponent<MapController>().endAnchor;
        maps.Add(go);
    }
}
