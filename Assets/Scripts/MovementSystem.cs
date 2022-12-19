using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MovementSystem : MonoBehaviour
{
    public GameObject player;

    public Transform spawnArea;

    public float speed;

    public float RightLeftMovementSpeed;

    public void Update()
    {
        //Not transform.forward due to the front being on the transform.right
        player.transform.position += speed * Time.deltaTime * transform.right;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            player.transform.position += RightLeftMovementSpeed * Time.deltaTime * transform.forward;

            spawnArea.transform.position = new Vector3(spawnArea.transform.position.x, spawnArea.transform.position.y, 0);
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            player.transform.position += RightLeftMovementSpeed * Time.deltaTime * -transform.forward;

            spawnArea.transform.position = new Vector3(spawnArea.transform.position.x, spawnArea.transform.position.y, 0);
        }
    }
}
