using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settings;
    public GameObject MM;

    public AudioSource music;

    void Start()
    {
        LeanTween.value(0, 1, 5f).setOnUpdate((float val) => music.volume = val);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Credits()
    {

    }

    public void Settings()
    {
        settings.SetActive(true);
        MM.SetActive(false);
    }

    public void ExitSettings()
    {
        settings.SetActive(false);
        MM.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }


    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }
}
