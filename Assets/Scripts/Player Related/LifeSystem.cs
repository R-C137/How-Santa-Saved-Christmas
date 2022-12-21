using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    public int lives = 3;

    public List<GameObject> hearts = new();

    public void Start()
    {
        for (int i = 0; i < lives; i++)
        {
            hearts[i].SetActive(true);
        }
    }

    public void RemoveLife()
    {
        int index = lives - 1;

        LeanTween.size(hearts[index].transform as RectTransform, new Vector2(150, 150), .2f).setOnComplete(() =>
        {
            LeanTween.size(hearts[index].transform as RectTransform, new Vector2(5, 5), .3f)
                .setOnComplete(() => hearts[index].SetActive(false));
        });

        if (lives == 1)
        {
            Utility.instance.SetGameOver();
            return;
        }

        lives--;
    }
}
