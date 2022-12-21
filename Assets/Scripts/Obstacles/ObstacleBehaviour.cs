using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : SpawnedObjectBehaviour
{
    public override void OnPlayerTrigger(Collider playerCollider)
    {
        playerCollider.gameObject.GetComponent<LifeSystem>().RemoveLife();
    }
}
