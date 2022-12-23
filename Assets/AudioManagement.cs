using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagement : MonoBehaviour
{
    public TextWriter TW;
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

    private void Update()
    {
        if (musicSource.time == musicSource.clip.length - 2f )
        {
            PlayMusic();
        }
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    public void PlayMuttering(AudioClip clip)
    {
        if(TW.finishedWriting == false)
        {
            sfxSource.clip = clip;
            sfxSource.Play();
        }
        else
        {
            sfxSource.Stop();
            return;
        }
    }

    // Play music
    public void PlayMusic()
    {
        musicSource.clip = BackgroundMusic;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }
}
