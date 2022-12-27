using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Singleton used for common functions and common fields such as "isPlayerDead"
public class Utility : MonoBehaviour
{
    public static Utility instance = null;

    public bool isGameOver = false;

    public delegate void GameOver();

    public event GameOver onGameOver;
    public bool hasGameEnded;
    
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

    public bool gameStarted;

    public GameObject TextHelper;

    public TextMeshProUGUI candytotal;

    public TextMeshProUGUI giftsTotal;

    public TextMeshProUGUI timerHS;

    public int currentHighScore;

    public GameObject deathUI;

    public TextMeshProUGUI deathUITimer;

    public TextMeshProUGUI currentLevelText;

    public GameObject playerPos;

    public GameObject cam;

    public GameObject blizFar;
    public GameObject blizNear;

    public Transform parent;

    public Image blackScreen;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        blackScreen.gameObject.SetActive(true);
    }

    void Start()
    {
        playerLevel = PlayerPrefs.GetInt("PlayerLevel", 0);

        onGameOver += () => totalTime = timeElasped;

        candytotal.text = PlayerPrefs.GetInt("CandyCanesTotal", 0).ToString();

        giftsTotal.text = PlayerPrefs.GetInt("GiftsTotal", 0).ToString();

        timerHS.text = PlayerPrefs.GetString("TimerHS", "0:0");

        currentHighScore = PlayerPrefs.GetInt("TimerHSScript", 0);

        currentLevelText.text = playerLevel.ToString();


        LeanTween.value(blackScreen.gameObject, 1, 0, 3.5f).setOnUpdate((float val) =>
                blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, val))
            .setOnComplete(() => blackScreen.gameObject.SetActive(false));

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

        LeanTween.moveY(levelupText.transform as RectTransform, finalLevelupYPos, 2.5f).setEase(ease).setOnComplete(() =>
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
        hasGameEnded = true;
        onRunEnded?.Invoke();

        EndRunAnimations();
    }

    public void StartGame()
    {
        onGameStarted?.Invoke();

        gameUI.SetActive(true);
        preGameUI.SetActive(false);

        gameStarted = true;
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

    void EndRunAnimations()
    {
        cam.transform.parent = parent;
        blizFar.transform.parent = parent;
        blizNear.transform.parent = parent;

        Vector3 fPos = new Vector3(playerPos.transform.position.x + 125, playerPos.transform.position.y + 90,
            playerPos.transform.position.z);

        LeanTween.move(playerPos, fPos, 8f);

        blackScreen.gameObject.SetActive(true);

        LeanTween.value(blackScreen.gameObject, 0, 1, 3.5f).setOnUpdate((float val) =>
                blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, val))
            .setOnComplete(() => LeanTween.value(0, 1, 1f).setOnComplete(() => SceneManager.LoadScene(1)));

        endRunButton.SetActive(false);
    }
}
