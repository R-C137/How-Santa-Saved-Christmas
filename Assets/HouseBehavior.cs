using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBehavior : SpawnedObjectBehaviour
{
    public Color houseColor;

    public Material SR;

    public bool Gifted;

    private void Start()
    {
        SR = GetComponent<Renderer>().material = new Material(Shader.Find("Universal Render Pipeline/Lit"));

        int HouseType = Random.Range(1, 5);

        switch (HouseType)
        {
            case 1:
                SR.color = Color.green;
                houseColor = Color.green;
                break;
            case 2:
                SR.color = Color.red;
                houseColor = Color.red;
                break;
            case 3:
                SR.color = Color.blue;
                houseColor = Color.blue;
                break;
            case 4:
                SR.color = Color.yellow;
                houseColor = Color.yellow;
                break;
            case 5:
                SR.color = Color.black;
                houseColor = Color.black;
                break;
        }

    }
}
