using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePowerupBehaviour : SpawnedObjectBehaviour
{
    public LeanTweenType type;

    void Start()
    {
        Rotate();
        MoveUpDown();
    }

    void Rotate()
    {
        LeanTween.rotateY(transform.parent.gameObject, 180, 2.5f)
            .setOnComplete(() => LeanTween.rotateY(transform.parent.gameObject, 0, 2.5f).setOnComplete(Rotate));
    }

    void MoveUpDown()
    {
        LeanTween.moveY(transform.parent.gameObject, 5, .5f).setOnComplete(() => LeanTween.moveY(transform.parent.gameObject, 3, .5f).setOnComplete(MoveUpDown));
    }

    public override void Update()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, overlapingSize);


        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == gameObject)
                continue;

            if (collider.gameObject.TryGetComponent<SpawnedObjectBehaviour>(out var spawnedObjectBehaviour))
            {
                OnOverlap(collider);

                if (destroySelfOnOverlap)
                {
                    if (!spawnedObjectBehaviour.destroySelfOnOverlap)
                    {
                        Destroy(transform.parent.gameObject, .1f);
                        destroying = true;
                    }
                    else
                    {
                        if (spawnedObjectBehaviour.destroying)
                            return;

                        if (!spawnedObjectBehaviour.CalculateDestroy())
                        {
                            Destroy(transform.parent.gameObject, .1f);
                            destroying = true;
                        }
                    }
                }
            }
        }
    }

    public override void OnPlayerTrigger(Collider playerCollider)
    {
        var ms = playerCollider.gameObject.GetComponent<MovementSystem>();

        ms.Slow();

        LeanTween.cancel(transform.parent.gameObject);
        LeanTween.moveY(transform.parent.gameObject, 10, .3f);
    }

    void OnDestroy()
    {
        LeanTween.cancel(transform.parent.gameObject);
    }
}
