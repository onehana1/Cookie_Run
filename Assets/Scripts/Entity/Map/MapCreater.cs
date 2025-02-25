using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapCreater : MonoBehaviour
{
    public GameObject[] mapPrefabs;//�� ������
    public Queue <GameObject> maps;//������ �ʵ�
    Transform firstEndAnchor = null;//ù��° ���� ������ ����
    Transform lastEndAnchor = null;//������ ���� ������ ����
    
    void Start()
    {
        GameObject go = transform.Find("Grounds").gameObject;
        if (go == null)
        {
            Debug.LogError("Can not find Grounds");
        }
        maps = new Queue <GameObject>();
        maps.Enqueue(go);
        lastEndAnchor = firstEndAnchor = maps.Peek().GetComponent<MapController>().endAnchor;//�⺻������ ����ִ� ���� ������ �ʱ�ȭ
        CreateMap();//�ʻ���
    }

    private void FixedUpdate()
    {
        if(firstEndAnchor.transform.position.x <= -10)//�� ����
        {
            GameObject go = maps.Peek();
            maps.Dequeue();
            Destroy(go);
            firstEndAnchor = maps.Peek().GetComponent<MapController>().endAnchor;
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
        maps.Enqueue(go);
    }
}
