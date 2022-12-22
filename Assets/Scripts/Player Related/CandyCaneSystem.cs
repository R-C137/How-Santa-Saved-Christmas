using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CandyCaneSystem : MonoBehaviour
{
    public TutorialManager tmgr;

    public AnimationCurve giftsNeededCurve;

    public int candyCaneCollected;

    public TextMeshProUGUI candyCaneCounter;

    public AnimationCurve candyCaneNeededCurve;
    
    void Start()
    {
        candyCaneCounter.text = $"{candyCaneCollected}/{Mathf.RoundToInt(candyCaneNeededCurve.Evaluate(Utility.instance.playerLevel))}";
    }

    public void GiftDropped(ref int giftsDropped)
    {
        if(giftsDropped >= Mathf.RoundToInt(giftsNeededCurve.Evaluate(Utility.instance.playerLevel)))
        {
            candyCaneCollected++;

            PlayerPrefs.SetInt("CandyCanesTotal", candyCaneCollected);

            candyCaneCounter.text =
                $"{candyCaneCollected}/{Mathf.RoundToInt(candyCaneNeededCurve.Evaluate(Utility.instance.playerLevel))}";

            giftsDropped = 0;

            if (Mathf.RoundToInt(candyCaneNeededCurve.Evaluate(Utility.instance.playerLevel)) == candyCaneCollected)
            {
                PlayerPrefs.SetInt("CandyCanes", PlayerPrefs.GetInt("CandyCanes") + candyCaneCollected);

                Utility.instance.LevelUp();

                candyCaneCollected = 0;
                giftsDropped = 0;

                Utility.instance.EnableRunEnder();

                tmgr.EndEnabled();

                candyCaneCounter.text =
                    $"{candyCaneCollected}/{Mathf.RoundToInt(candyCaneNeededCurve.Evaluate(Utility.instance.playerLevel))}";
            }
        }
    }
}
