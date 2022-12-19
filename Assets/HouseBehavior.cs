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
        SR = GetComponent<Renderer>().material = new Material(Shader.Find("Universal Render Pipeline/Lit"));

        HouseType = Random.Range(1, 5);

        switch (HouseType)
        {
            case 1:
                SR.color = Color.green;
                break;
            case 2:
                SR.color = Color.red;
                break;
            case 3:
                SR.color = Color.blue;
                break;
            case 4:
                SR.color = Color.yellow;
                break;
            case 5:
                SR.color = Color.black;
                break;
        }

    }
}
