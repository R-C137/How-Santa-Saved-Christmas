using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CandyCaneSystem : MonoBehaviour
{
    public AnimationCurve giftsNeededCurve;

    public int candyCaneCollected;

    public TextMeshProUGUI candyCaneCounter;

    public AnimationCurve candyCaneNeededCurve;

    public void GiftDropped(int giftsDropped)
    {
        if(giftsDropped >= Mathf.RoundToInt(giftsNeededCurve.Evaluate(Utility.instance.playerLevel)))
        {
            candyCaneCounter.text = candyCaneCollected.ToString();
            candyCaneCollected++;
        }
    }
}
