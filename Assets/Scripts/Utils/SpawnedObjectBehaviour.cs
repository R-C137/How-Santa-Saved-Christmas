using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedObjectBehaviour : MonoBehaviour
{
    public TMPro.TMP_Text ded;

    public bool destroySelfOnOverlap = true;

    public virtual void OnPlayerTrigger(Collider playerCollider)
    {
        if(this.CompareTag("Obstacle"))
        {

            if (ded != null)
                ded.gameObject.SetActive(true);
            playerCollider.GetComponentInParent<MovementSystem>().Stop = true;
            //playerCollider.GetComponent<MovementSystem>().SpeedSlider.interactable = false;
        }
    }

    public virtual void OnOverlapTrigger(Collider spawnedObjectCollider)
    {

    }

    public virtual void OnUnknownTrigger(Collider other)
    {

    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            OnPlayerTrigger(other);
        else if (other.TryGetComponent<SpawnedObjectBehaviour>(out _))
        {
            OnOverlapTrigger(other);

            if (destroySelfOnOverlap)
                Destroy(gameObject);
        }
        else
            OnUnknownTrigger(other);
    }

}
