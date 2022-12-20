using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : SpawnedObjectBehaviour
{
    public override void OnPlayerTrigger(Collider playerCollider)
    {
        var lifeSystem = playerCollider.gameObject.GetComponent<LifeSystem>();

        if (lifeSystem.lives == 0)
        {
            Utility.instance.SetGameOver();
        }
        else
            lifeSystem.lives--;
    }
}
