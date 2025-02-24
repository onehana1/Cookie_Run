using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeGameScene()
    {
        SceneManager.LoadScene("PHN_Scene"); 
    }
    public void ChangeLobbyScene()
    {
        SceneManager.LoadScene("02.LobbyScene"); 
    }
}