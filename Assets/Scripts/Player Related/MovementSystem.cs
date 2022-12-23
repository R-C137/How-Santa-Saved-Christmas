using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Cinemachine;

public class MovementSystem : MonoBehaviour
{
    public CinemachineVirtualCamera playerCam;
    public int maxCamTilt;
    public float tiltSpeed;
    public float timeElapsed;
    bool resetClick;

    public GameObject player;

    public Transform spawnArea;

    public float speed;

    public float rightLeftMovementSpeed;

    public bool stopped = true;

    public Collider movementBounds;

    private Vector3 oldPos;

    public GameObject blizzardNear;
    public GameObject blizzardFar;

    void Start()
    {
        Utility.instance.onGameOver += () => stopped = true;

        Utility.instance.onRunEnded += () => stopped = true;

        Utility.instance.onGameStarted += () => stopped = false;

        Utility.instance.onGamePaused += () => stopped = true;

        Utility.instance.onGameUnPaused += () => stopped = false;

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

            if (timeElapsed < tiltSpeed)
            {
                playerCam.m_Lens.Dutch = Mathf.Lerp(playerCam.m_Lens.Dutch, maxCamTilt, timeElapsed / tiltSpeed);
                timeElapsed += Time.deltaTime;
            }
            else
            {
                timeElapsed = 0;
                playerCam.m_Lens.Dutch = maxCamTilt;
            }

            if (player.transform.position.z > 8)
                player.transform.position = new Vector3(player.transform.position.x, oldPos.y, oldPos.z);
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            player.transform.position += rightLeftMovementSpeed * Time.deltaTime * -transform.forward;

            if (timeElapsed < tiltSpeed)
            {
                playerCam.m_Lens.Dutch = Mathf.Lerp(playerCam.m_Lens.Dutch, -maxCamTilt, timeElapsed / tiltSpeed);
                timeElapsed += Time.deltaTime;
            }
            else
            {
                timeElapsed = 0;
                playerCam.m_Lens.Dutch = -maxCamTilt;
            }

            if (player.transform.position.z < -8)
            player.transform.position = new Vector3(player.transform.position.x, oldPos.y, oldPos.z);
        }

        spawnArea.transform.position = new Vector3(spawnArea.transform.position.x, spawnArea.transform.position.y, 0);
        //movementBounds.transform.position = new Vector3(movementBounds.transform.position.x, movementBounds.transform.position.y, 0);

        blizzardNear.transform.position = new Vector3(blizzardNear.transform.position.x, blizzardNear.transform.position.y, 0);
        blizzardFar.transform.position = new Vector3(blizzardFar.transform.position.x, blizzardFar.transform.position.y, 0);


        //if (!movementBounds.bounds.Contains(player.transform.position))
        //{
        //    player.transform.position = new Vector3(player.transform.position.x, oldPos.y, oldPos.z);
        //}

    }
}
