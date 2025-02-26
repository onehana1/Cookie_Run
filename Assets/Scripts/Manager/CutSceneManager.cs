using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            ChangeScene.ChangeResultScene();
        }
    }
}
