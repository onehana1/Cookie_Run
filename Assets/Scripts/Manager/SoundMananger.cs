using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundMananger : MonoBehaviour
{
    public static SoundMananger instance; //�̱���

    [Header("Title")]
    [SerializeField] private AudioSource titleEffect;//Ÿ��Ʋ �Ҹ�

    [Header("BGM")]//BGM ���
    [SerializeField] private AudioSource lobbyBGM;//�κ�BGM
    [SerializeField] private AudioSource inGameBGM;//�ΰ���BGM
    [SerializeField] private AudioSource clearBGM;//Ŭ����BGM
    [SerializeField] private AudioSource onBGM;//�������� BGM

    [Header("Effect")]//Effect ���
    [SerializeField] private AudioSource jumpEffect;//����Effect
    [SerializeField] private AudioSource obstacleEffect;//�浹Effect
    [SerializeField] private AudioSource jellyEffect;//����Effect
    [SerializeField] private AudioSource coinEffect;//����Effect
    [SerializeField] private AudioSource healEffect;//��Effect
    [SerializeField] private AudioSource clickEffect;//Ŭ��Effect
    [SerializeField] private AudioSource sceneEffect;//��Effec
    [SerializeField] private AudioSource magnetEffect;//�ڼ�Effect
    [SerializeField] private AudioSource giantEffect;//����Effect
    [SerializeField] private AudioSource gallopingEffect;//����Effect
    [SerializeField] private AudioSource resqueEffect;//Ż��Effect
    [SerializeField] private AudioSource choiceEffect;//����Effect
    [SerializeField] private AudioSource skillCorrectEffect;//�̴ϰ�������Effect
    [SerializeField] private AudioSource skillIncorrectEffect;//�̴ϰ��ӿ���Effect
    [SerializeField] private AudioSource lobbyEffect;//�κ�Effect
    [SerializeField] private AudioSource pauseEffect;//�Ͻ�����Effect
    [SerializeField] private AudioSource clearNPCEffect;//Ŭ����NPCEffect
    [SerializeField] private AudioSource destoryEffect;//��ֹ��ı�Effect
    [SerializeField] private AudioSource[] teacherEffect;//����Effect

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    //���� ũ��
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
        //���� ũ�� �ε�
        MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        BGMVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        EffectVolume = PlayerPrefs.GetFloat("EffectVolume", 1f);

        //���� ũ�� ����
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
        while (titleEffect.isPlaying)//titleEffect�� �Ϸ�Ǹ� ClearBGM �÷���
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
