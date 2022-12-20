using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        Debug.Log("Settings menu not yet created");
    }

    public void Credits()
    {
        Debug.Log("Credits menu not yet created");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
