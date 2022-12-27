using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseRandom : MonoBehaviour
{

    private void Start()
    {
        this.GetComponent<ParticleSystemRenderer>().enabled = Random.Range(0, 6) == 0 ? true : false;

    }
}
