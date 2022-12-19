using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MovementSystem : MonoBehaviour
{
    public GameObject player;

    public float speed;

    public void Update()
    {
        //Not transform.forward due to the front being on the transform.right
        player.transform.position += transform.right * speed * Time.deltaTime;
    }
}
