using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfBehavior : SpawnedObjectBehaviour
{

    public float AttackSpeed;
    public float AttackTime;

    public MovementSystem playerMovement;

    public Collider movementBounds;

    public float speed, lateralMovementSpeed;

    public bool CanAttack;

    private void Start()
    {

        lateralMovementSpeed = playerMovement.rightLeftMovementSpeed;
        speed = playerMovement.speed;

        destroySelfOnOverlap = false;

        StartCoroutine(WaitForAttack());
    }

    public override void Update()
    {

        base.Update();

        this.transform.position += speed * Time.deltaTime * transform.right;

        transform.position += lateralMovementSpeed * Time.deltaTime * transform.forward;

        movementBounds.transform.position = new Vector3(movementBounds.transform.position.x, movementBounds.transform.position.y, 0);

        if (!movementBounds.bounds.Contains(transform.position))
        {
            lateralMovementSpeed = -lateralMovementSpeed;
        }
    }

    IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(AttackTime);
        lateralMovementSpeed = 0;
        speed = -speed;

        yield return new WaitForSeconds(AttackTime + 4);
        Destroy(this.gameObject);
    }


    public override void OnPlayerTrigger(Collider playerCollider)
    {
        playerCollider.gameObject.GetComponent<LifeSystem>().RemoveLife();
    }
}
