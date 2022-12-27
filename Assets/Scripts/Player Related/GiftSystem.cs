using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GiftSystem : MonoBehaviour
{
    public TutorialManager tmgr;

    public AudioClip GiftSFX;

    public AudioManagement AudioSystem;

    public CandyCaneSystem candyCaneSystem;

    public Color CurrentColor;

    public int giftsDropped;

    public List<GameObject> giftsUI = new();

    public GameObject oldGift = null;

    public TextMeshProUGUI gifsCounter;

    public void Awake()
    {
        CurrentColor = Color.green;
        SetSelected(giftsUI[0]);
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
            CurrentColor = new Color32(255, 255, 0, 255);
            SetSelected(giftsUI[3]);
        }
    }

    public void DropGift(bool right)
    {
        giftsDropped++;

        PlayerPrefs.SetInt("GiftsTotal", giftsDropped);

        candyCaneSystem.GiftDropped(ref giftsDropped);

        // when a gift is dropped, play the animation
        GetComponent<Animator>().SetTrigger("DropGift");

        AudioSystem.PlaySFX(GiftSFX);

        tmgr.GiftDropped();
    }

}
