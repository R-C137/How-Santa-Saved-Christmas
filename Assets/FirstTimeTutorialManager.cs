using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTimeTutorialManager : MonoBehaviour
{
    public GameObject firstTime;

    void Start()
    {
        if (PlayerPrefs.GetInt("FirstTime", 0) == 1)
        {
            firstTime.SetActive(false);
        }
        else
            PlayerPrefs.SetInt("FirstTime", 1);
    }
}
