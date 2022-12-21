using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerupBehaviour : SpawnedObjectBehaviour
{
    public override void OnPlayerTrigger(Collider playerCollider)
    {
        var ls = playerCollider.gameObject.GetComponent<LifeSystem>();

        if (ls.lives != 3)
        {
            ls.AddLife();

            LeanTween.rotateY(gameObject, 360, 1f).setDestroyOnComplete(true);

            LeanTween.moveY(gameObject, 10, .3f);
        }
    }
}
