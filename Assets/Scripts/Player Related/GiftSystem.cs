using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GiftSystem : MonoBehaviour
{
    public Color CurrentColor;

    public int giftsDropped;

    public TextMeshProUGUI timer;

    private DateTime timeElasped;
    
    public void Awake()
    {
        Utility.instance.onGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        Utility.instance.totalTime = timeElasped;
    }

    private void Update()
    {
        if(Utility.instance.isGameOver)
            return;;

        timeElasped = timeElasped.AddSeconds(Time.deltaTime);

        timer.text = $"{timeElasped.Minute + timeElasped.Hour * 60}:{timeElasped.Second}:{timeElasped.Millisecond}";

        if (Input.GetKeyDown(KeyCode.Alpha1)/* && !hasChosen*/)
        {
            CurrentColor = Color.green;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)/* && !hasChosen*/)
        {
            CurrentColor = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)/* && !hasChosen*/)
        {
            CurrentColor = Color.blue;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)/* && !hasChosen*/)
        {
            CurrentColor = Color.yellow;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)/* && !hasChosen*/)
        {
            CurrentColor = Color.black;
        }
    }

    public void DropGift()
    {
        giftsDropped++;
    }

}
