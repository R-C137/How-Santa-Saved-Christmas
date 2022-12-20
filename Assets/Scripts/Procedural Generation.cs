using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class ProceduralGeneration : MonoBehaviour
{
    public GameObject player;

    public List<GameObject> proceduralGenerations = new();

    public List<GameObject> objects = new();

    public float nextPosToDestroy = -1;

    public float spawnPosOffset = 10;

    public Vector3 spawnOffset;

    public void Update()
    {
        if (player.transform.position.x >= objects.Last().transform.position.x - spawnPosOffset)
        {
            SpawnNewProceduralTerrain();
        }

        if (player.transform.position.x >= nextPosToDestroy && nextPosToDestroy != -1f)
        {
            Destroy(objects[0]);
            objects.RemoveAt(0);

            nextPosToDestroy = -1;
        }
    }

    void SpawnNewProceduralTerrain()
    {
        GameObject currentProcudualObj =
            Instantiate(proceduralGenerations[Random.Range(0, proceduralGenerations.Count - 1)]);

        GameObject latestGameObject = objects.Last();

        float endPosOfLatestGameobject = latestGameObject.transform.position.x + latestGameObject.transform.lossyScale.x / 2;

        currentProcudualObj.transform.position =
            new Vector3(endPosOfLatestGameobject + currentProcudualObj.transform.lossyScale.x / 2, 0, 0) + spawnOffset;

        objects.Add(currentProcudualObj);

        nextPosToDestroy = endPosOfLatestGameobject + /*Destroy offset = */ 5f;
        
    }
}
