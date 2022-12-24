using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : SpawnedObjectBehaviour
{
    public AudioClip Oh;
    public AudioManagement AB;
    public override void OnPlayerTrigger(Collider playerCollider)
    {
        AB.PlaySFX(Oh);

        playerCollider.gameObject.GetComponent<LifeSystem>().RemoveLife();
    }
}
