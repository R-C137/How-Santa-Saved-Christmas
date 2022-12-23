using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HouseBehavior : SpawnedObjectBehaviour
{
    public Color HouseColor;

    public Material SR;

    public float alpha = 255;

    public bool Gifted;

    private void Start()
    {
        SR = GetComponent<Renderer>().material;

        int colorIndex = Random.Range(0, 4);

        switch (colorIndex)
        {
            case 0:
                HouseColor = new Color32(255, 0, 0, (byte)alpha); //red
                break;
            case 1:
                HouseColor = new Color32(0, 255, 0, (byte)alpha); //green
                break;
            case 2:
                HouseColor = new Color32(0, 0, 255, (byte)alpha); //blue
                break;
            case 3:
                HouseColor = new Color32(255, 255, 0, (byte)alpha); //yellow
                break;

        }
        
        SR.color = new (HouseColor.r, HouseColor.g, HouseColor.b, HouseColor.a);
        SR.SetColor("_EmissionColor", new Color((byte)HouseColor.r, (byte)HouseColor.g, (byte)HouseColor.b));
    }

    void OnTriggerStay(Collider other)
    {
        if(Gifted)
            return;;
        if (other.CompareTag("Player"))
        {
            var dg = other.GetComponent<GiftSystem>();

            if (Input.GetButton("Jump") && HouseColor.CompareRGB(dg.CurrentColor) /*&& canDrop*/)
            {
                bool right = transform.position.z == 9;

                dg.DropGift(right);
                Gifted = true;
                //canDrop = false;
                //StartCoroutine(ResetDrop());
            }
        }
    }
}