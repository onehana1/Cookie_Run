using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMananger : MonoBehaviour
{
    public static SoundMananger instance;

    [Header("BGM")]
    [SerializeField] private AudioSource lobbyBGM;
    [SerializeField] private AudioSource inGameBGM;
    [SerializeField] private AudioSource onBGM;

    [Header("Effect")]
    [SerializeField] private AudioSource jumpEffect;
    [SerializeField] private AudioSource obstacleEffect;
    [SerializeField] private AudioSource jellyEffect;
    [SerializeField] private AudioSource coinEffect;
    [SerializeField] private AudioSource itemEffect;
    [SerializeField] private AudioSource clickEffect;

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
        inGameBGM.Play();
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
}
