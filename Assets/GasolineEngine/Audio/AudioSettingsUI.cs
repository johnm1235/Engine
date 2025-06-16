using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {

        masterSlider.value = PlayerPrefs.GetFloat("MasterVol", 0.75f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVol", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVol", 0.75f);

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMasterVolume(float value)
    {
        AudioManager.Instance.SetVolume("MasterVol", value);
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.Instance.SetVolume("MusicVol", value);
    }

    public void SetSFXVolume(float value)
    {
        AudioManager.Instance.SetVolume("SFXVol", value);
    }
}
