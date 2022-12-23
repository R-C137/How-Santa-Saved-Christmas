using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextWriter : MonoBehaviour
{
    public List<string> texts = new();

    public KeyCode nextTextKey = KeyCode.Return;

    public float timePerWord = .2f;

    public TextMeshProUGUI textShower;

    public int currentIndex = 0;

    public Coroutine routine;

    public bool finishedWriting;

    public Action finishWritingCallback;

    public bool destroySelfOnEnd;

    public AudioSource gibberishSFX;

    void Start()
    {
        textShower = GetComponent<TextMeshProUGUI>();

        if(texts.Any())
            routine = StartCoroutine(WriteText(texts[currentIndex]));
    }

    void Update()
    {
        if (Input.GetKeyDown(nextTextKey) && (currentIndex != texts.Count - 1  || !finishedWriting))
        {
            if (finishedWriting)
            {
                currentIndex++;

                routine = StartCoroutine(WriteText(texts[currentIndex]));
            }
            else
            {
                StopCoroutine(routine);
                textShower.text = texts[currentIndex];

                finishedWriting = true;
            }
        }
        else if(Input.GetKeyDown(nextTextKey) && currentIndex == texts.Count - 1 && finishedWriting)
        {
            finishWritingCallback?.Invoke();

            if(destroySelfOnEnd)
                Destroy(transform.parent.gameObject);
        }

        if(!finishedWriting && !gibberishSFX.isPlaying)
            gibberishSFX.Play();
        else if(finishedWriting && gibberishSFX.isPlaying)
            gibberishSFX.Pause();
    }

    IEnumerator WriteText(string text)
    {
        finishedWriting = false;
        textShower.text = "";
        foreach (char character in text)
        {
            yield return new WaitForSeconds(timePerWord);

            textShower.text = $"{textShower.text}{character}";

        }

        finishedWriting = true;
    }
}
