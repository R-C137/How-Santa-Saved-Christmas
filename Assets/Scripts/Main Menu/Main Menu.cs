using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject settings;
    public GameObject MM;

    public AudioSource music;

    public GameObject credits;

    public GameObject intro;

    public Image blackScreen;

    void Start()
    {
        FadeIn();
    }

    void FadeIn(float speed = 5f)
    {
        LeanTween.cancel(gameObject);

        music.Play();
        LeanTween.value(gameObject, 0, 1, speed).setOnUpdate((float val) => music.volume = val);
    }

    void FadeOut(float speed = 5f)
    {
        LeanTween.cancel(gameObject);
        LeanTween.value(gameObject, 1, 0, speed).setOnUpdate((float val) => music.volume = val).setOnComplete(() => music.Stop());
    }

    public void Play()
    {
        if (PlayerPrefs.GetInt("PlayIntro", 1) == 1)
        {
            FadeOut();

            LeanTween.value(blackScreen.gameObject, 0, 1, 2.5f).setOnUpdate((float val) =>
                    blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, val))
                .setOnComplete(PlayIntro);
        }
        else
        {
            LeanTween.value(blackScreen.gameObject, 0, 1, 2.5f).setOnUpdate((float val) =>
                    blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, val))
                .setOnComplete(() => SceneManager.LoadScene(1));
        }
    }

    public void PlayIntro()
    {
        blackScreen.gameObject.SetActive(false);

        PlayerPrefs.SetInt("PlayIntro", 0);

        intro.SetActive(true);

        LeanTween.delayedCall(gameObject, 86f, () => SceneManager.LoadScene(1));
    }

    public void Credits()
    {
        credits.SetActive(true);
        MM.SetActive(false);

        FadeOut(2f);

        LeanTween.delayedCall(gameObject, 46f, () =>
        {
            Debug.Log("called");
            credits.SetActive(false);
            MM.SetActive(true);
            FadeIn();
        });
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
