using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeMainScene()
    {
        SceneManager.LoadScene("PHN_Scene"); 
    }
    public void ChangeLobbyScene()
    {
        SceneManager.LoadScene("LobbyScene"); 
    }
}