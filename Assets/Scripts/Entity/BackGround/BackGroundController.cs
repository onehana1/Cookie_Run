using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    const float BACKGOUND_OBJECT_PIVOT = -14.5f;//��� �̹����� ������ ��ġ
    const float BACKGOUND_IMAGE_PIVOT = -23f;//

    public float moveSpeed;//�̵��ӵ� ���
    public float backGroundImageMoveSpeed;//��� �̹��� �̵��ӵ�
    public float backGroundLayer1MoveSpeed;//��� ������Ʈ1 �̵��ӵ�
    public float backGroundLayer2MoveSpeed;//��� ������Ʈ2 �̵��ӵ�
    public float landMoveSpeed;//�� �̵��ӵ�;

    private float backGroundImageWidth;
    private float backGroundObjectWidth;

    public GameObject backGroundImage;
    public GameObject backGroundLayer1;
    public GameObject backGroundLayer2;
    public GameObject land;
    public GameObject jelly;

    void Start()
    {
        Time.timeScale = 1.0f;//�ð��� ���� ����
        SpriteRenderer spriteRenderer = backGroundImage.GetComponentInChildren<SpriteRenderer>();
        backGroundImageWidth = spriteRenderer.bounds.size.x;//��� �̹����� ��
        spriteRenderer = backGroundLayer1.GetComponentInChildren<SpriteRenderer>();
        backGroundObjectWidth = spriteRenderer.bounds.size.x;//��� ������Ʈ�� ��
    }

    private void FixedUpdate()
    {
        MoveBackGround();//����̵�
    }

    private void MoveBackGround()
    {
        Move(backGroundImage, backGroundImageMoveSpeed, backGroundImageWidth, 0);//��� �̹��� �̵�
        Move(backGroundLayer1, backGroundLayer1MoveSpeed, backGroundObjectWidth, 1);//��� ������Ʈ1 �̵�
        Move(backGroundLayer2, backGroundLayer2MoveSpeed, backGroundObjectWidth, 1);//��� ������Ʈ2 �̵�
        
        land.transform.position -= new Vector3(landMoveSpeed * moveSpeed, 0, 0);//���� ���������� ���������̶� ���� ����
        jelly.transform.position -= new Vector3(landMoveSpeed * moveSpeed, 0, 0);//���� ���������� ���������̶� ���� ����
    }

    private void Move(GameObject gameObject, float speed, float width, int pivot)
    {
        Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
        Transform parentTransform = gameObject.transform;
        float objectPivot;//�������� �������� ��ġ

        if (pivot == 0)//��� �̹����϶�
        {
            objectPivot = BACKGOUND_IMAGE_PIVOT;
        }
        else if (pivot == 1)//��� ������Ʈ�϶�
        {
            objectPivot = BACKGOUND_OBJECT_PIVOT;
        }
        else//�߸��� ��
        {
            Debug.LogError("wrong pivot");
            return;
        }

        foreach (Transform tf in transforms)
        {
            if (tf != parentTransform)//�ڽ� ������Ʈ�� �̵�
            {
                tf.transform.position -= new Vector3(speed * moveSpeed, 0, 0);//�̵�
                if (tf.transform.position.x <= objectPivot)//���� ��ġ�� �����ϸ�
                {
                    tf.transform.position = new Vector3(tf.transform.position.x + width * (transforms.Length - 1),
                        tf.transform.position.y, 0);//��ġ ����
                }
            }
        }
        return;
    }
}
