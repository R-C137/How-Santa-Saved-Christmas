using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropGifts : MonoBehaviour
{
    public TMP_Text currentCol;

    public TMP_Text GiftsDroppedTxt;

    public Color CurrentColor;

    public float ResetTimer;
    public float giftDropTimer;

    //public bool hasChosen;

    public int giftsDropped;

    //public bool canDrop;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)/* && !hasChosen*/)
        {
            CurrentColor = Color.green; // green
            currentCol.text = "Color: green";
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)/* && !hasChosen*/)
        {
            CurrentColor = Color.red; // red
           currentCol.text = "Color: red";
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)/* && !hasChosen*/)
        {
            CurrentColor = Color.blue; // blue
           currentCol.text = "Color: blue";
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)/* && !hasChosen*/)
        {
            CurrentColor = Color.yellow; // yellow
            currentCol.text = "Color: yellow";
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)/* && !hasChosen*/)
        {
            CurrentColor = Color.black; // black
            currentCol.text = "Color: black";
        }
    }

    //public void ChangeGift(int color)
    //{
    //    CurrentColor = color;
    //    //hasChosen = true;
    //    //StartCoroutine(ResetCounter());
    //}

    //IEnumerator ResetCounter()
    //{
    //    yield return new WaitForSeconds(ResetTimer);

    //    hasChosen = false;

    //    print("timer resetted");
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("House"))
        {
            if (Input.GetButton("Jump") &&  other.GetComponent<HouseBehavior>().Gifted != true && Input.GetButton("Jump") && other.GetComponent<HouseBehavior>().houseColor == CurrentColor /*&& canDrop*/)
            {
                giftsDropped++;
                GiftsDroppedTxt.text = "gifts dropped: " + giftsDropped;
                other.GetComponent<HouseBehavior>().Gifted = true;
                //canDrop = false;
                //StartCoroutine(ResetDrop());
            }
        }
    }

    //IEnumerator ResetDrop()
    //{
    //    yield return new WaitForSeconds(giftDropTimer);
    //    canDrop = true;
    //}


}
