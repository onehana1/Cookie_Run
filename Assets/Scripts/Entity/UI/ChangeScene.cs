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
        SceneManager.LoadScene("LobbyScene");
    }
    
    public void ChangeScoreScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("ScoreScene");
    }

    public void ChangeResultGoodScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(7);
    }
    public void ChangeResultBadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(6);
    }
    public void ChangeChoiceScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("ChoiceScene");
    }
}