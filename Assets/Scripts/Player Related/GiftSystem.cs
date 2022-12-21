using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GiftSystem : MonoBehaviour
{
    public CandyCaneSystem candyCaneSystem;

    public Color CurrentColor;

    public int giftsDropped;

    public TextMeshProUGUI timer;

    private DateTime timeElasped;

    public List<GameObject> giftsUI = new();

    public GameObject oldGift = null;

    public TextMeshProUGUI gifsCounter;

    public void Awake()
    {
        Utility.instance.onGameOver += OnGameOver;

        CurrentColor = Color.green;
        SetSelected(giftsUI[0]);
    }

    private void OnGameOver()
    {
        Utility.instance.totalTime = timeElasped;
    }

    void SetSelected(GameObject obj)
    {
        if (oldGift == obj)
            return;

        (obj.transform as RectTransform).sizeDelta = new Vector2(170, 170);

        if (oldGift != null)
        {
            (oldGift.transform as RectTransform).sizeDelta = new Vector2(145, 145);
        }

        oldGift = obj;
    }

    private void Update()
    {
        if(Utility.instance.isGameOver)
            return;;

        gifsCounter.text = $"{giftsDropped}/{Mathf.RoundToInt(candyCaneSystem.giftsNeededCurve.Evaluate(Utility.instance.playerLevel))} Gifts";

        timeElasped = timeElasped.AddSeconds(Time.deltaTime);

        timer.text = $"{timeElasped.Minute + timeElasped.Hour * 60}:{timeElasped.Second}";

        if (Input.GetKeyDown(KeyCode.Alpha1)/* && !hasChosen*/)
        {
            CurrentColor = Color.green;
            SetSelected(giftsUI[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)/* && !hasChosen*/)
        {
            CurrentColor = Color.red;
            SetSelected(giftsUI[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)/* && !hasChosen*/)
        {
            CurrentColor = Color.blue;
            SetSelected(giftsUI[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)/* && !hasChosen*/)
        {
            CurrentColor = Color.yellow;
            SetSelected(giftsUI[3]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)/* && !hasChosen*/)
        {
            CurrentColor = Color.black;
            SetSelected(giftsUI[4]);
        }
    }

    public void DropGift()
    {
        giftsDropped++;
        candyCaneSystem.GiftDropped(giftsDropped);
    }

}
