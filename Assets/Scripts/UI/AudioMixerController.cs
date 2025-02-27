using System.Collections;
using System.Collections.Generic;
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
        }
        if (BGMSlider != null)
        {
            BGMSlider.onValueChanged.AddListener(SetBGMValue);
        }
        if (EffectSlider != null)
        {
            EffectSlider.onValueChanged.AddListener(SetEffectValue);
        }
    }

    public void SetMasterValue(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetBGMValue(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetEffectValue(float volume)
    {
        audioMixer.SetFloat("Effect", Mathf.Log10(volume) * 20);
    }
}
