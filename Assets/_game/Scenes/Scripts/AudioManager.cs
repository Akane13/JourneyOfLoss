using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;
    public List<AudioSource> sfxSources;
    public int sfxPoolSize = 10; // Adjust based on your needs

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSFXSources();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSFXSources()
    {
        sfxSources = new List<AudioSource>();
        for (int i = 0; i < sfxPoolSize; i++)
        {
            AudioSource newSfxSource = gameObject.AddComponent<AudioSource>();
            sfxSources.Add(newSfxSource);
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        AudioSource availableSource = GetAvailableSFXSource();
        if (availableSource != null)
        {
            availableSource.PlayOneShot(clip);
        }
    }

    public void SetBGMVolume(float volume)
    {
        Debug.Log($"Setting BGM volume to: {volume}");
        bgmSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        foreach (var sfxSource in sfxSources)
        {
            sfxSource.volume = volume;
        }
    }

    public void MuteAll(bool isMuted)
    {
        Debug.Log($"Muting all: {isMuted}");
        AudioListener.pause = isMuted;
    }

    private AudioSource GetAvailableSFXSource()
    {
        foreach (var source in sfxSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        return null;
    }
}
