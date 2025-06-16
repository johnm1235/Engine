using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixer audioMixer;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip defaultMusic;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre escenas
        }
        else
        {
            Destroy(gameObject);
        }

        ApplySavedVolumes();

        if (defaultMusic != null)
            PlayMusic(defaultMusic);
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void StopSFX(AudioClip clip)
    {
        sfxSource.Stop();
    }

    public void ApplySavedVolumes()
    {
        SetVolume("MasterVol", PlayerPrefs.GetFloat("MasterVol", 0.75f));
        SetVolume("MusicVol", PlayerPrefs.GetFloat("MusicVol", 0.75f));
        SetVolume("SFXVol", PlayerPrefs.GetFloat("SFXVol", 0.75f));
    }

    public void SetVolume(string parameter, float value)
    {
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat(parameter, dB);
        PlayerPrefs.SetFloat(parameter, value);
    }
}
