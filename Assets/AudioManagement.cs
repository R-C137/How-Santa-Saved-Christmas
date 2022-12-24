using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagement : MonoBehaviour
{
    public TextWriter TW;
    public static AudioManagement instance;

    public List<AudioClip> BackgroundMusic = new();

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
    
    // Play music
    public void PlayMusic()
    {
        musicSource.clip = BackgroundMusic[Random.Range(0, BackgroundMusic.Count - 1)];
        musicSource.volume = musicVolume;
        musicSource.Play();
    }
}
