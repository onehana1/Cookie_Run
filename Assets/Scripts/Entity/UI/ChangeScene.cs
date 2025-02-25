using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeGameScene()
    { 
        Time.timeScale = 1;
        SceneManager.LoadScene("CHB_Scene"); 
    }
    public void ChangeLobbyScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("02.LobbyScene");
    }
    public void ChangeScoreScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("ScoreScene");
    }
}