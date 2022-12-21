using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagement : MonoBehaviour
{
    public static AudioManagement instance;

    public AudioClip BackgroundMusic;

    // Audio source for music
    public AudioSource musicSource;

    // Audio source for sound effects
    public AudioSource sfxSource;

    // Volume for music
    [Range(0f, 10f)]
    public float musicVolume = 1f;

    // Volume for sound effects
    [Range(0f, 10f)]
    public float sfxVolume = 1f;

    void Awake()
    {
        // Set the singleton instance
        instance = this;

        PlayMusic();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    // Play music
    public void PlayMusic()
    {
        musicSource.clip = BackgroundMusic;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }
}
