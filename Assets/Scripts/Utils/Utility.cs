using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Singleton used for common functions and common fields such as "isPlayerDead"
public class Utility : MonoBehaviour
{
    public static Utility instance = null;

    public bool isGameOver = false;

    public delegate void GameOver();

    public event GameOver onGameOver;
    
    public delegate void RunEnded();

    public event RunEnded onRunEnded;

    public delegate void GameStarted();

    public event GameStarted onGameStarted;

    public delegate void GamePaused();

    public event GamePaused onGamePaused;

    public delegate void GameUnPaused();

    public event GameUnPaused onGameUnPaused;

    public DateTime totalTime;

    public int playerLevel;

    public TextMeshProUGUI levelupText;

    public float defaultLevelupYPos;

    public float finalLevelupYPos;

    public LeanTweenType ease;

    public bool levelFinished;

    public TextMeshProUGUI timer;

    public GameObject endRunButton;

    public GameObject gameUI;

    public GameObject preGameUI;

    private DateTime timeElasped;

    public bool runEnded;

    public bool isPaused;

    public bool preGame = true;

    public GameObject TextHelper;

    public TextMeshProUGUI candytotal;

    public TextMeshProUGUI giftsTotal;

    public TextMeshProUGUI timerHS;

    public int currentHighScore;

    public GameObject deathUI;

    public TextMeshProUGUI deathUITimer;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        playerLevel = PlayerPrefs.GetInt("PlayerLevel", 0);

        onGameOver += () => totalTime = timeElasped;

        candytotal.text = PlayerPrefs.GetInt("CandyCanesTotal", 0).ToString();

        giftsTotal.text = PlayerPrefs.GetInt("GiftsTotal", 0).ToString();

        timerHS.text = PlayerPrefs.GetString("TimerHS", "0:0");

        currentHighScore = PlayerPrefs.GetInt("TimerHSScript", 0);
    }

    void Update()
    {
        if (!preGame && !isGameOver && !isPaused)
        {
            timeElasped = timeElasped.AddSeconds(Time.deltaTime);

            timer.text = $"{timeElasped.Minute + timeElasped.Hour * 60}:{timeElasped.Second}";

            int totalTimer = timeElasped.Minute + timeElasped.Hour * 60 + timeElasped.Second;

            if (currentHighScore < totalTimer)
            {
                PlayerPrefs.SetString("TimerHS", $"{timeElasped.Minute + timeElasped.Hour * 60}:{timeElasped.Second}");

                PlayerPrefs.SetInt("TimerHSScript", totalTimer);
            }
        }

        if (preGame && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) ||
                        Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            StartGame();
            preGame = false;
        }
    }

    public void EnableRunEnder()
    {
        endRunButton.SetActive(true);
        levelFinished = true;
    }

    public void SetGameOver()
    {
        isGameOver = true;
        onGameOver?.Invoke();

        deathUI.SetActive(true);
        gameUI.SetActive(false);

        deathUITimer.text = timer.text;
    }

    public void LevelUp()
    {
        levelupText.gameObject.SetActive(true);

        (levelupText.transform as RectTransform).position =
            new((levelupText.transform as RectTransform).transform.position.x, defaultLevelupYPos);

        LeanTween.moveY(levelupText.transform as RectTransform, finalLevelupYPos, .5f).setEase(ease).setOnComplete(() =>
        {
            levelupText.gameObject.SetActive(false);
        });

        playerLevel++;

        PlayerPrefs.SetInt("PlayerLevel", playerLevel);
    }

    public void EndRun()
    {
        gameUI.SetActive(false);
        runEnded = true;
        onRunEnded?.Invoke();
    }

    public void StartGame()
    {
        onGameStarted?.Invoke();

        gameUI.SetActive(true);
        preGameUI.SetActive(false);
    }

    public void SetPause(bool pause)
    {
        if (pause)
        {
            onGamePaused?.Invoke();
            isPaused = true;
        }
        else
        {
            onGameUnPaused?.Invoke();
            isPaused = false;
        }
    }

    public void ShowHelpText(List<string> texts, bool destroyOnFinish, Action onFinish)
    {
        GameObject obj = Instantiate(TextHelper, gameUI.transform);

        obj.transform.GetChild(0).GetComponent<TextWriter>().texts = texts;
        obj.transform.GetChild(0).GetComponent<TextWriter>().finishWritingCallback = onFinish;
        obj.transform.GetChild(0).GetComponent<TextWriter>().destroySelfOnEnd = destroyOnFinish;
    }
}
