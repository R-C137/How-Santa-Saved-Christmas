using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowSpawner : MonoBehaviour
{
    public List<Transform> startPos = new();
    public List<Transform> endPos = new();

    public Transform canvas;

    public GameObject snowball;
    public float timeToEndPos;

    public float spawnTimer = 1;

    public bool showGizmos = true;

    void Awake()
    {
        StartCoroutine(SpawnSnowballs());
    }

    void OnDrawGizmos()
    {
        if (showGizmos)
        {
            for (int i = 0; i < startPos.Count; i++)
            {
                Vector3 start = startPos[i].position;
                Vector3 end = endPos[i].position;

                Gizmos.DrawLine(start, end);
            }
        }
    }

    IEnumerator SpawnSnowballs()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTimer);

            GameObject obj = Instantiate(snowball, canvas);

            int index = Random.Range(0, startPos.Count - 1);

            obj.transform.position = startPos[index].position;
            
            LeanTween.moveY(obj, endPos[index].transform.position.y, timeToEndPos).setDestroyOnComplete(true);
        }
    }
}
