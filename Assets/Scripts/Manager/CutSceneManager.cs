using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    ChangeScene _changeScene;

    private void Start()
    {
        _changeScene = new ChangeScene();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            _changeScene.ChangeResultScene();
        }
    }
}
