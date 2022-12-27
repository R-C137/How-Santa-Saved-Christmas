using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfBehavior : SpawnedObjectBehaviour
{
    public bool isOnRight;

    public float AttackSpeed;
    public float AttackTime;

    public MovementSystem playerMovement;

    public Collider movementBounds;

    public float speed, lateralMovementSpeed;

    public bool CanAttack;

    public float rotationAngle;
    

    private void Start()
    {

        lateralMovementSpeed = playerMovement.rightLeftMovementSpeed;
        speed = playerMovement.speed;

        destroySelfOnOverlap = false;

        StartCoroutine(WaitForAttack());
    }

    public override void Update()
    {
        if (Utility.instance.isGameOver || Utility.instance.runEnded || Utility.instance.isPaused || !Utility.instance.gameStarted)
            return;

        base.Update();
        this.transform.position += speed * Time.deltaTime * transform.right;

        transform.position += lateralMovementSpeed * Time.deltaTime * transform.forward;

        Quaternion rot = transform.rotation;

        /*if(isOnRight)
        {
            transform.rotation = new Quaternion(rotationAngle, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        }*/

        movementBounds.transform.position = new Vector3(movementBounds.transform.position.x, movementBounds.transform.position.y, 0);

        if (!movementBounds.bounds.Contains(transform.position))
        {
            lateralMovementSpeed = -lateralMovementSpeed;
            /*
            isOnRight = false;
            if (!isOnRight)
            {
                transform.rotation = new Quaternion(-rotationAngle, transform.rotation.y, transform.rotation.z, transform.rotation.w);
            }*/
        } /*else
        {
            isOnRight = true;
        }*/
            
    }

    IEnumerator WaitForAttack()
    {
        if (!Utility.instance.isGameOver || !Utility.instance.runEnded || !Utility.instance.isPaused || Utility.instance.gameStarted)
        {
            yield return new WaitForSeconds(AttackTime);
            lateralMovementSpeed = 0;
            speed = -speed;

            yield return new WaitForSeconds(AttackTime + 4);
            Destroy(this.gameObject);
        }
    }


    public override void OnPlayerTrigger(Collider playerCollider)
    {
        playerCollider.gameObject.GetComponent<LifeSystem>().RemoveLife();
    }
}
