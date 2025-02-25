using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    const float BACKGOUND_OBJECT_PIVOT = -14.5f;//배경 이미지의 재조정 위치
    const float BACKGOUND_IMAGE_PIVOT = -23f;//

    public float moveSpeed;//이동속도 배수
    public float backGroundImageMoveSpeed;//배경 이미지 이동속도
    public float backGroundLayer1MoveSpeed;//배경 오브젝트1 이동속도
    public float backGroundLayer2MoveSpeed;//배경 오브젝트2 이동속도
    public float landMoveSpeed;//땅 이동속도;

    private float backGroundImageWidth;
    private float backGroundObjectWidth;

    public GameObject backGroundImage;
    public GameObject backGroundLayer1;
    public GameObject backGroundLayer2;
    public GameObject land;
    public GameObject jelly;

    void Start()
    {
        Time.timeScale = 1.0f;//시간에 따라 증가
        SpriteRenderer spriteRenderer = backGroundImage.GetComponentInChildren<SpriteRenderer>();
        backGroundImageWidth = spriteRenderer.bounds.size.x;//배경 이미지의 폭
        spriteRenderer = backGroundLayer1.GetComponentInChildren<SpriteRenderer>();
        backGroundObjectWidth = spriteRenderer.bounds.size.x;//배경 오브젝트의 폭
    }

    private void FixedUpdate()
    {
        MoveBackGround();//배경이동
    }

    private void MoveBackGround()
    {
        Move(backGroundImage, backGroundImageMoveSpeed, backGroundImageWidth, 0);//배경 이미지 이동
        Move(backGroundLayer1, backGroundLayer1MoveSpeed, backGroundObjectWidth, 1);//배경 오브젝트1 이동
        Move(backGroundLayer2, backGroundLayer2MoveSpeed, backGroundObjectWidth, 1);//배경 오브젝트2 이동
        
        land.transform.position -= new Vector3(landMoveSpeed * moveSpeed, 0, 0);//땅은 프리팹으로 관리예정이라 삭제 예정
        jelly.transform.position -= new Vector3(landMoveSpeed * moveSpeed, 0, 0);//땅은 프리팹으로 관리예정이라 삭제 예정
    }

    private void Move(GameObject gameObject, float speed, float width, int pivot)
    {
        Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
        Transform parentTransform = gameObject.transform;
        float objectPivot;//포지션을 재조정할 위치

        if (pivot == 0)//배경 이미지일때
        {
            objectPivot = BACKGOUND_IMAGE_PIVOT;
        }
        else if (pivot == 1)//배경 오브젝트일때
        {
            objectPivot = BACKGOUND_OBJECT_PIVOT;
        }
        else//잘못된 값
        {
            Debug.LogError("wrong pivot");
            return;
        }

        foreach (Transform tf in transforms)
        {
            if (tf != parentTransform)//자식 오브젝트만 이동
            {
                tf.transform.position -= new Vector3(speed * moveSpeed, 0, 0);//이동
                if (tf.transform.position.x <= objectPivot)//일정 위치에 도달하면
                {
                    tf.transform.position = new Vector3(tf.transform.position.x + width * (transforms.Length - 1),
                        tf.transform.position.y, 0);//위치 조정
                }
            }
        }
        return;
    }
}
