using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HouseBehavior : SpawnedObjectBehaviour
{
    public Color HouseColor;

    public Material SR;

    public float alpha = 255f;

    public bool Gifted;

    private void Start()
    {
        SR = GetComponent<Renderer>().material;

        int colorIndex = Random.Range(0, 3);

        switch (colorIndex)
        {
            case 0:
                HouseColor = Color.red;
                break;
            case 1:
                HouseColor = Color.blue;
                break;
            case 2:
                HouseColor = Color.green;
                break;
            case 3:
                HouseColor = Color.yellow;
                break;

        }
        
        SR.color = new(HouseColor.r, HouseColor.g, HouseColor.b, alpha/255);
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
                dg.DropGift();
                Gifted = true;
                //canDrop = false;
                //StartCoroutine(ResetDrop());
            }
        }
    }
}