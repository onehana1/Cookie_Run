using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float magnetDuration = 5f; // �ڼ� ȿ�� ���� �ð�
    public float magnetRange = 5f; // �ڼ� ����
    public float attractionSpeed = 5f; // �������� �������� �ӵ�

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾ �ڼ��� ������
        {
            SoundMananger.instance.PlayItemEffect();
            StartCoroutine(ActivateMagnet(other.gameObject)); // �ڼ� ȿ�� Ȱ��ȭ
            Destroy(gameObject); // �ڼ� ������ ����
        }
    }

    private IEnumerator ActivateMagnet(GameObject player)
    {
        MagnetEffect magnet = player.GetComponent<MagnetEffect>();
        if (magnet == null)
        {
            magnet = player.AddComponent<MagnetEffect>(); // �÷��̾�� MagnetEffect �߰�
        }

        magnet.EnableMagnet(magnetRange, attractionSpeed, magnetDuration); // �ڼ� ȿ�� ����
        yield return new WaitForSeconds(magnetDuration); // 5�� ��
        //magnet.DisableMagnet(); // �ڼ� ȿ�� ����
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
