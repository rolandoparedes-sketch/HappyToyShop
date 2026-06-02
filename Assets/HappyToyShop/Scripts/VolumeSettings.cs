using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [FoldoutGroup("MasterVolumeSettings")]
    [SerializeField] private Slider masterMusicSlider;

    [FoldoutGroup("MasterVolumeSettings")]

    [SerializeField] private Slider masterSFXSlider;


    [FoldoutGroup("VolumeSettings")]

    [SerializeField] private Slider musicSlider;
    [FoldoutGroup("VolumeSettings")]

    [SerializeField] private Slider sfxSlider;
    [FoldoutGroup("VolumeSettings")]

    [SerializeField] private Slider ambientSlider;
    [FoldoutGroup("VolumeSettings")]

    [SerializeField] private Slider voiceSlider;
    [FoldoutGroup("VolumeSettings")]

    [SerializeField] private Slider uiSlider;


    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Toggle AdvanceLevel;

    void Start()
    {
        AdvanceLevel.isOn = false;

    }

    void Update()
    {
        
    }
    public void SetMasterMusicVolume()
    {
        float volume = masterMusicSlider.value;

        audioMixer.SetFloat("MasterMusic", Mathf.Log10(volume)*20);
    }


    public void SetMasterSFXVolume()
    {
        float volume = masterSFXSlider.value;

        audioMixer.SetFloat("MasterSFX", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;

        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }
    public void SetAmbientVolume()
    {
        float volume = ambientSlider.value;

        audioMixer.SetFloat("Ambient", Mathf.Log10(volume) * 20);
    }
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;

        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
    public void SetUIVolume()
    {
        float volume = uiSlider.value;

        audioMixer.SetFloat("UI", Mathf.Log10(volume) * 20);
    }
    public void SetVoiceVolume()
    {
        float volume = voiceSlider.value;

        audioMixer.SetFloat("Voice", Mathf.Log10(volume) * 20);
    }

    public void ActiveAdvancedSettings()
    {
        if(!AdvanceLevel.isOn)
        {
            masterMusicSlider.gameObject.SetActive(true);
            masterSFXSlider.gameObject.SetActive(true);

            musicSlider.gameObject.SetActive(false);
            sfxSlider.gameObject.SetActive(false);
            ambientSlider.gameObject.SetActive(false);
            voiceSlider.gameObject.SetActive(false);
            uiSlider.gameObject.SetActive(false);
        }
        if (AdvanceLevel.isOn)
        {
            masterMusicSlider.gameObject.SetActive(false);
            masterSFXSlider.gameObject.SetActive(false);

            musicSlider.gameObject.SetActive(true);
            sfxSlider.gameObject.SetActive(true);
            ambientSlider.gameObject.SetActive(true);
            voiceSlider.gameObject.SetActive(true);
            uiSlider.gameObject.SetActive(true);
        }

    
    }

}
