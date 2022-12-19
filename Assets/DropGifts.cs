using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropGifts : MonoBehaviour
{
    public TMP_Text currentCol;

    public TMP_Text GiftsDroppedTxt;

    public int CurrentColor;

    public float ResetTimer;
    public float giftDropTimer;

    public bool hasChosen;

    public int giftsDropped;

    public bool canDrop;


    private void Update()
    {
        if (Input.GetButtonDown("1") && !hasChosen)
        {
            ChangeGift(1); // green
            currentCol.text = "Color: green";
        }
        if (Input.GetButtonDown("2") && !hasChosen)
        {
            ChangeGift(2); // red
            currentCol.text = "Color: red";
        }
        if (Input.GetButtonDown("3") && !hasChosen)
        {
            ChangeGift(3); // blue
            currentCol.text = "Color: blue";
        }
        if (Input.GetButtonDown("4") && !hasChosen)
        {
            ChangeGift(4); // yellow
            currentCol.text = "Color: yellow";
        }
        if (Input.GetButtonDown("5") && !hasChosen)
        {
            ChangeGift(5); // black
            currentCol.text = "Color: black";
        }
    }

    public void ChangeGift(int color)
    {
        CurrentColor = color;
        hasChosen = true;
        StartCoroutine(ResetCounter());
    }

    IEnumerator ResetCounter()
    {
        yield return new WaitForSeconds(ResetTimer);

        hasChosen = false;

        print("timer resetted");

        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("House"))
        {
            if (Input.GetButton("Jump") &&  other.GetComponent<HouseBehavior>().Gifted != true && Input.GetButton("Jump") && other.GetComponent<HouseBehavior>().HouseType == CurrentColor && canDrop)
            {
                giftsDropped++;
                GiftsDroppedTxt.text = "gifts dropped: " + giftsDropped.ToString();
                other.GetComponent<HouseBehavior>().Gifted = true;
                canDrop = false;
                StartCoroutine(ResetDrop());
            }
        }
    }

    IEnumerator ResetDrop()
    {
        yield return new WaitForSeconds(giftDropTimer);
        canDrop = true;
        yield break;
    }


}
