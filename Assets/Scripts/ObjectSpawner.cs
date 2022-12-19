
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    public Transform player;

    public List<GameObject> objects = new();

    public List<GameObject> spawnedObjects = new();

    public Collider bounds;

    //WILL CAUSE AN INFINITE WHILE LOOP IF SET TO 0 AND POSSIBLY AN ERROR IF SET TO LESS
    public float spawnTimer = 1;

    public Transform parent;

    public void Awake()
    {
        StartCoroutine(SpawnObjectsTimer());
    }

    public Vector3 GetRandomPos()
    {
        return new Vector3(
            Random.Range(bounds.bounds.min.x, bounds.bounds.max.x),
            Random.Range(bounds.bounds.min.y, bounds.bounds.max.y),
            Random.Range(bounds.bounds.min.z, bounds.bounds.max.z)
        );
    }

    IEnumerator SpawnObjectsTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTimer);

            Vector3 pos = GetRandomPos();

            GameObject obj = Instantiate(objects[Random.Range(0, objects.Count - 1)]);

            obj.transform.position = pos;

            obj.transform.position += new Vector3(0, obj.transform.lossyScale.y / 2, 0);

            obj.transform.parent = parent;

            spawnedObjects.Add(obj);

            spawnedObjects.RemoveAll((_obj) => _obj == null);

            //ToList() to prevent editing from the actual list and only a copy
            foreach (GameObject @object in spawnedObjects.ToList())
            {

                if (@object.transform.position.x < player.transform.position.x - /* offset */ 20f)
                {
                    spawnedObjects.Remove(@object);
                    Destroy(@object);
                }
            }
        }
    }
}
