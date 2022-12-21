using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class SpawnedObjectBehaviour : MonoBehaviour
{
    public bool destroySelfOnOverlap = true;
    
    public bool compensateForHeight = true;

    public bool destroying;

    public Vector3 overlapingSize;

    public virtual void OnPlayerTrigger(Collider playerCollider)
    {
        //if (this.CompareTag("Player"))
        //{

        //    if (ded != null)
        //        ded.gameObject.SetActive(true);
        //    playerCollider.GetComponentInParent<MovementSystem>().stopped = true;
        //    //playerCollider.GetComponent<MovementSystem>().SpeedSlider.interactable = false;
        //}

    }

    public virtual void Update()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, overlapingSize);


        foreach (Collider collider in colliders)
        {
            if(collider.gameObject == gameObject)
                continue;
            
            if (collider.gameObject.TryGetComponent<SpawnedObjectBehaviour>(out var spawnedObjectBehaviour))
            {
                OnOverlap(collider);

                if (destroySelfOnOverlap)
                {
                    if (!spawnedObjectBehaviour.destroySelfOnOverlap)
                    {
                        Destroy(gameObject, .1f);
                        destroying = true;
                    }
                    else
                    {
                        if (spawnedObjectBehaviour.destroying)
                            return;

                        if (!spawnedObjectBehaviour.CalculateDestroy())
                        {
                            Destroy(gameObject, .1f);
                            destroying = true;
                        }
                    }
                }
            }
        }
    }

    public virtual void OnOverlap(Collider spawnedObjectCollider)
    {

    }

    public virtual void OnUnknownTrigger(Collider other)
    {

    }

    public virtual bool CalculateDestroy()
    {
        int rng = Random.Range(0, 1);

        if (rng == 0)
            return false;
        else
        {
            Destroy(gameObject, .1f);
            destroying = true;
            return true;
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            OnPlayerTrigger(other);
        else
            OnUnknownTrigger(other);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, overlapingSize);
    }

}
