using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerupBehaviour : SpawnedObjectBehaviour
{
    public LeanTweenType type;

    void Start()
    {
        Rotate();
        MoveUpDown();
    }

    void Rotate()
    {
        LeanTween.rotateY(gameObject, 180, 2.5f)
            .setOnComplete(() => LeanTween.rotateY(gameObject, 0, 2.5f).setOnComplete(Rotate));
    }

    void MoveUpDown()
    {
        LeanTween.moveY(gameObject, 5, .5f).setOnComplete(() => LeanTween.moveY(gameObject, 3, .5f).setOnComplete(MoveUpDown));
    }

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
