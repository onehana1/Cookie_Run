using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain_JellyController : MonoBehaviour
{
    public int scoreValue = 25; // ������ ������ ��� ����

    //public int scoreValue = 25; // ���� ����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �浹�� ��ü�� Player���� Ȯ��
        {
            //GameManager.Instance.AddScore(scoreValue); // ���� �߰�
            Destroy(gameObject); // ���� ����
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
