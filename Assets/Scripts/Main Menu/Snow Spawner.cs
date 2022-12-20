using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnowSpawner : MonoBehaviour
{
    public List<Transform> startPos = new();
    public List<Transform> endPos = new();

    public Transform parent;
    public GameObject snowball;

    public float timeToEndPos = 2.5f;

    public float spawnTimer = .2f;

    public bool showGizmos = true;

    void Start()
    {
        StartCoroutine(SpawnSnowballs());
    }

    void OnDrawGizmos()
    {
        if (showGizmos)
            for (int i = 0; i < startPos.Count; i++)
            {
                if (endPos.Any() && startPos.Count >= i)
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

            GameObject obj = Instantiate(snowball, parent);

            int index = Random.Range(0, endPos.Count - 1);

            obj.transform.position = startPos[index].position;

            LeanTween.moveY(obj, endPos[index].transform.position.y, timeToEndPos)
                .setDestroyOnComplete(true);
        }
    }
}
