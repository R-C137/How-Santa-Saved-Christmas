using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBehavior : MonoBehaviour
{
    public int HouseType;

    public Material SR;

    public bool Gifted;

    private void Start()
    {
        SR = GetComponent<Renderer>().material;

        HouseType = Random.Range(1, 6);

        switch (HouseType)
        {
            case 1:
                SR.color = new Color32(0, 255, 0, 100);
                break;
            case 2:
                SR.color = new Color32(255, 0, 0, 100);
                break;
            case 3:
                SR.color = new Color32(0, 0, 255, 100); 
                break;
            case 4:
                SR.color = new Color32(0, 255, 255, 100);
                break;
            case 5:
                SR.color = new Color32(0, 0, 0, 100);
                break;
        }

    }
}
