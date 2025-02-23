using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{

    public float moveSpeed;
    public float BackGroundImageMoveSpeed;
    public float BackGroundLayer1MoveSpeed;
    public float BackGroundLayer2MoveSpeed;
    public float LandMoveSpeed;

    public GameObject BackGroundImage;
    public GameObject BackGroundLayer1;
    public GameObject BackGroundLayer2;
    public GameObject Land;


    void Start()
    {
        Time.timeScale = 1.0f;
    }

    private void FixedUpdate()
    {
        MoveBackGround();
    }

    private void MoveBackGround()
    {
        BackGroundImage.transform.position -= new Vector3(BackGroundImageMoveSpeed * moveSpeed, 0, 0);
        BackGroundLayer1.transform.position -= new Vector3(BackGroundLayer1MoveSpeed * moveSpeed, 0, 0);
        BackGroundLayer2.transform.position -= new Vector3(BackGroundLayer2MoveSpeed * moveSpeed, 0, 0);
        Land.transform.position -= new Vector3(LandMoveSpeed * moveSpeed, 0, 0);
    }
}
