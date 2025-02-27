using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundMananger : MonoBehaviour
{
    public static SoundMananger instance;

    [Header("Title")]
    [SerializeField] private AudioSource titleEffect;

    [Header("BGM")]
    [SerializeField] private AudioSource lobbyBGM;
    [SerializeField] private AudioSource inGameBGM;
    [SerializeField] private AudioSource clearBGM;
    [SerializeField] private AudioSource onBGM;

    [Header("Effect")]
    [SerializeField] private AudioSource jumpEffect;
    [SerializeField] private AudioSource obstacleEffect;
    [SerializeField] private AudioSource jellyEffect;
    [SerializeField] private AudioSource coinEffect;
    [SerializeField] private AudioSource itemEffect;
    [SerializeField] private AudioSource clickEffect;
    [SerializeField] private AudioSource sceneEffect;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "01.StartScene")
        {
            PlayTitle();
            StartCoroutine(OnTitle());
        }
    }

    IEnumerator OnTitle()
    {
        while (titleEffect.isPlaying)
        {
            yield return null;
        }
        PlayClearBGM();
    }

    public void PlayTitle()
    {
        titleEffect.Play();
    }

    public void PlayLobbyBGM()
    {
        if (onBGM == lobbyBGM)
        {
            return;
        }
        if (onBGM != null)
        {
            onBGM.Stop();
        }
        onBGM = lobbyBGM;
        onBGM.Play();
    }

    public void PlayInGameBGM()
    {
        if (onBGM == inGameBGM)
        {
            return;
        }
        if (onBGM != null)
        {
            onBGM.Stop();
        }
        onBGM = inGameBGM;
        onBGM.Play();
    }

    public void PlayClearBGM()
    {
        if (onBGM == clearBGM)
        {
            return;
        }
        if (onBGM != null)
        {
            onBGM.Stop();
        }
        onBGM = clearBGM;
        onBGM.Play();
    }

    public void PlayJumpEffect()
    {
        jumpEffect.Play();
    }

    public void PlayObstacleEffect()
    {
        obstacleEffect.Play();
    }

    public void PlayJellyEffect() 
    { 
        jellyEffect.Play(); 
    }

    public void PlayCoinEffect()
    {
        coinEffect.Play();
    }

    public void PlayItemEffect()
    {
        itemEffect.Play();
    }

    public void PlayClickEffect()
    {
        clickEffect.Play();
    }

    public void PlaySceneEffect()
    {
        sceneEffect.Play();
    }
}
