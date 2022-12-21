using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MovementSystem : MonoBehaviour
{
    public GameObject player;

    public Transform spawnArea;

    public float speed;

    public float rightLeftMovementSpeed;

    public bool stopped;

    public Collider movementBounds;

    private Vector3 oldPos;

    void Start()
    {
        Utility.instance.onGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        stopped = true;
    }

    public void Update()
    {
        if(stopped)
            return;

        oldPos = player.transform.position;

        //Not transform.forward due to the front being on the transform.right
        player.transform.position += speed * Time.deltaTime * transform.right;
        

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            player.transform.position += rightLeftMovementSpeed * Time.deltaTime * transform.forward;
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            player.transform.position += rightLeftMovementSpeed * Time.deltaTime * -transform.forward;
        }

        spawnArea.transform.position = new Vector3(spawnArea.transform.position.x, spawnArea.transform.position.y, 0);
        movementBounds.transform.position = new Vector3(movementBounds.transform.position.x, movementBounds.transform.position.y, 0);

        if (!movementBounds.bounds.Contains(player.transform.position))
        {
            player.transform.position = new Vector3(player.transform.position.x, oldPos.y, oldPos.z);
        }
    }
}
