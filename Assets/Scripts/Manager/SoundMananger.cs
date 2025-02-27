using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundMananger : MonoBehaviour
{
    public static SoundMananger instance; //싱글톤

    [Header("Title")]
    [SerializeField] private AudioSource titleEffect;//타이틀 소리

    [Header("BGM")]//BGM 목록
    [SerializeField] private AudioSource lobbyBGM;//로비BGM
    [SerializeField] private AudioSource inGameBGM;//인게임BGM
    [SerializeField] private AudioSource clearBGM;//클리어BGM
    [SerializeField] private AudioSource onBGM;//실행중인 BGM

    [Header("Effect")]//Effect 목록
    [SerializeField] private AudioSource jumpEffect;//점프Effect
    [SerializeField] private AudioSource obstacleEffect;//충돌Effect
    [SerializeField] private AudioSource jellyEffect;//젤리Effect
    [SerializeField] private AudioSource coinEffect;//코인Effect
    [SerializeField] private AudioSource healEffect;//힐Effect
    [SerializeField] private AudioSource clickEffect;//클릭Effect
    [SerializeField] private AudioSource sceneEffect;//씬Effec
    [SerializeField] private AudioSource magnetEffect;//자석Effect
    [SerializeField] private AudioSource giantEffect;//거인Effect
    [SerializeField] private AudioSource gallopingEffect;//돌진Effect
    [SerializeField] private AudioSource resqueEffect;//탈출Effect
    [SerializeField] private AudioSource choiceEffect;//선택Effect
    [SerializeField] private AudioSource skillCorrectEffect;//미니게임정답Effect
    [SerializeField] private AudioSource skillIncorrectEffect;//미니게임오답Effect
    [SerializeField] private AudioSource lobbyEffect;//로비Effect
    [SerializeField] private AudioSource pauseEffect;//일시정지Effect
    [SerializeField] private AudioSource clearNPCEffect;//클리어NPCEffect
    [SerializeField] private AudioSource destoryEffect;//장애물파괴Effect
    [SerializeField] private AudioSource[] teacherEffect;//훈장Effect

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    //음향 크기
    public float MasterVolume;
    public float BGMVolume;
    public float EffectVolume;

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
        //음향 크기 로드
        MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        BGMVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        EffectVolume = PlayerPrefs.GetFloat("EffectVolume", 1f);

        //음향 크기 설정
        audioMixer.SetFloat("Master", Mathf.Log10(MasterVolume) * 20);
        audioMixer.SetFloat("BGM", Mathf.Log10(BGMVolume) * 20);
        audioMixer.SetFloat("Effect", Mathf.Log10(EffectVolume) * 20);

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "01.StartScene")
        {
            PlayTitle();
            StartCoroutine(OnTitlePlay(scene.name));
        }
    }

    IEnumerator OnTitlePlay(string name)
    {
        Scene scene = SceneManager.GetActiveScene();
        while (titleEffect.isPlaying)//titleEffect가 완료되면 ClearBGM 플래이
        {
            yield return null;
            if (scene.name != name)
            {
                titleEffect.Stop();
            }
        }
        if (scene.name == name)
        {
            PlayClearBGM();
        }
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

    public void PlayHealEffect()
    {
        healEffect.Play();
    }

    public void PlayClickEffect()
    {
        clickEffect.Play();
    }

    public void PlaySceneEffect()
    {
        sceneEffect.Play();
    }

    public void PlayMagnetEffect()
    {
        magnetEffect.Play();
    }

    public void PlayGiantEffect()
    {
        giantEffect.Play();
    }

    public void PlayGallopingEffect()
    {
        gallopingEffect.Play();
    }

    public void PlayResqueEffect()
    {
        resqueEffect.Play();
    }

    public void PlayChoiceEffect()
    {
        choiceEffect.Play();
    }

    public void PlaySkillCorrectEffect()
    {
        skillCorrectEffect.Play();
    }

    public void PlaySkillIncorrectEffect()
    {
        skillIncorrectEffect.Play();
    }

    public void PlayLobbyEffect()
    {
        lobbyEffect.Play();
    }

    public void PlayPauseEffect()
    {
        pauseEffect.Play();
    }

    public void PlayTeacherEffect()
    {
        int num = UnityEngine.Random.Range(0, teacherEffect.Length);
        teacherEffect[num].Play();
    }

    public void PlayDestroyEffect()
    {
        destoryEffect.Play();
    }

    public void PlayNPCEffect()
    {
        clearNPCEffect.Play();
    }
}
