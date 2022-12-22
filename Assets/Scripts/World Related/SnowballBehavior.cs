using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Elf")
        {
            Destroy(other.gameObject);
        }
    }
}
