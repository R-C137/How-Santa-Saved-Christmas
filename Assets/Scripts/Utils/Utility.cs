using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Singleton used for common functions and common fields such as "isPlayerDead"
public class Utility : MonoBehaviour
{
    public static Utility instance = null;

    public bool isGameOver = false;

    public delegate void GameOver();

    public event GameOver onGameOver;

    void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }

    void SetGameOver()
    {
        isGameOver = true;
        onGameOver?.Invoke();
    }
}
