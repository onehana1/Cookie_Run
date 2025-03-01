using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{   
    public static void ChangeLobbyScene()
    {
        Time.timeScale = 1;
        SoundMananger.instance.PlayLobbyEffect();
        SoundMananger.instance.PlayLobbyBGM();
        SceneManager.LoadScene("02.LobbyScene");
    }

    public static void ChangeChoiceScene()
    {
        Time.timeScale = 1;
        SoundMananger.instance.PlayChoiceEffect();
        SoundMananger.instance.PlayLobbyBGM();
        SceneManager.LoadScene("03.StageScene");
    }

    public static void ChangeGameScene()
    {
        Time.timeScale = 1;
        SoundMananger.instance.PlaySceneEffect();
        SoundMananger.instance.PlayInGameBGM();
        SceneManager.LoadScene("04.GameScene");
    }

    public static void ChangeResultScene()
    {
        Time.timeScale = 1;
        SoundMananger.instance.PlaySceneEffect();
        SoundMananger.instance.PlayLobbyBGM();
        SceneManager.LoadScene("05.ResultScene");
    }

    public static void ChangeResultGoodScene()
    {
        Time.timeScale = 1;
        SoundMananger.instance.PlayClearBGM();
        SoundMananger.instance.PlayNPCEffect();
        SceneManager.LoadScene("ResultCutScene_Good");
    }
    public static void ChangeResultBadScene()
    {
        Time.timeScale = 1;
        SoundMananger.instance.PlayTeacherEffect();
        SoundMananger.instance.PlayLobbyBGM();
        SceneManager.LoadScene("ResultCutScene");
    }

}