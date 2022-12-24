using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class HoverSFx : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI hoveredText;

    public AudioSource sfx;

    void Awake()
    {
        hoveredText = GetComponent<TextMeshProUGUI>();
        sfx = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoveredText.fontSize += 3;
        sfx.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoveredText.fontSize -= 3;
    }
}
