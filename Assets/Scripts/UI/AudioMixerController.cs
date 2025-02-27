using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerController : MonoBehaviour
{
    [Header("AudioMixer")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider EffectSlider;

    private void Awake()
    {
        MasterSlider = transform.Find("MasterSlider").GetComponent<Slider>();
        BGMSlider = transform.Find("BGMSlider").GetComponent<Slider>();
        EffectSlider = transform.Find("EffectSlider").GetComponent<Slider>();

        if (MasterSlider != null)
        {
            MasterSlider.onValueChanged.AddListener(SetMasterValue);
            MasterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
            SetMasterValue(MasterSlider.value);
        }
        if (BGMSlider != null)
        {
            BGMSlider.onValueChanged.AddListener(SetBGMValue);
            BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
            SetBGMValue(BGMSlider.value);
        }
        if (EffectSlider != null)
        {
            EffectSlider.onValueChanged.AddListener(SetEffectValue);
            EffectSlider.value = PlayerPrefs.GetFloat("EffectVolume", 1f);
            SetEffectValue(EffectSlider.value);
        }
    }

    public void SetMasterValue(float volume)
    {
        PlayerPrefs.SetFloat("MasterVolume", volume);
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetBGMValue(float volume)
    {
        PlayerPrefs.SetFloat("BGNVolume", volume);
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetEffectValue(float volume)
    {
        PlayerPrefs.SetFloat("EffectVolume", volume);
        audioMixer.SetFloat("Effect", Mathf.Log10(volume) * 20);
    }
}
