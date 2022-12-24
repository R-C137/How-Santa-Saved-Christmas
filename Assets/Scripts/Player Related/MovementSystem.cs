using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Cinemachine;
using UnityEngine.UI;

public class MovementSystem : MonoBehaviour
{
    public List<Animator> reindeerAnim;



    public Image panel;
    public float timeBeforeEndLevel;
    public Utility util;

    public Canvas GameCanvas;

    public GameObject player;

    public Transform spawnArea;

    public float speed;

    public float rightLeftMovementSpeed;

    public bool stopped = true;

    public Collider movementBounds;

    private Vector3 oldPos;

    public GameObject blizzardNear;
    public GameObject blizzardFar;

    public int maxSpeed;
    public int maxRLSpeed;

    public float timeToMaxSpeed;

    public bool tweening;

    void Start()
    {
        Utility.instance.onGameOver += () => stopped = true;

        Utility.instance.onRunEnded += () => stopped = true;

        Utility.instance.onGameStarted += () => stopped = false;

        Utility.instance.onGamePaused += () => stopped = true;

        Utility.instance.onGameUnPaused += () => stopped = false;

    }

    public void Slow()
    {
        float amount;

        float level = PlayerPrefs.GetInt("TimeUpgradeCurrentLevel", 0);

        if (level <= 1)
            amount = 2f; //Normal length
        else if (level == 2)
            amount = 4f;
        else if (level == 3)
            amount = 8f;
        else
            amount = 10f;

        StartCoroutine(SlowSpeed(amount));
    }

    public void Update()
    {
        if (Utility.instance.isPaused || !Utility.instance.gameStarted)
        {
            foreach(Animator obj in reindeerAnim) { obj.SetBool("canRun", false); }
            return;
        }

        if (stopped)
            return;

        if (!tweening)
        {
            LeanTween.value(gameObject, speed, maxSpeed, timeToMaxSpeed).setOnUpdate((float val) =>
            {
                speed = val;
                rightLeftMovementSpeed = Mathf.Clamp(val, rightLeftMovementSpeed, maxRLSpeed);
            });

            tweening = true;
        }

        oldPos = player.transform.position;

        //Not transform.forward due to the front being on the transform.right
        player.transform.position += speed * Time.deltaTime * transform.right;
        foreach (Animator obj in reindeerAnim) { obj.SetBool("canRun", true); }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            player.transform.position += rightLeftMovementSpeed * Time.deltaTime * transform.forward;

            if (player.transform.position.z > 8)
                player.transform.position = new Vector3(player.transform.position.x, oldPos.y, oldPos.z);
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            player.transform.position += rightLeftMovementSpeed * Time.deltaTime * -transform.forward;

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
    IEnumerator SlowSpeed(float slow)
    {
        float oldSpeed = speed;
        float oldRLSpeed = rightLeftMovementSpeed;

        speed -= slow;

        rightLeftMovementSpeed -= slow;

        LeanTween.pause(gameObject);

        yield return new WaitForSeconds(8f);

        speed = oldSpeed;

        rightLeftMovementSpeed = oldRLSpeed;

        LeanTween.resume(gameObject);
    }
}
