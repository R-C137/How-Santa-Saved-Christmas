
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Jobs;
using Unity.Collections;
using UnityEngine;

public enum SpawningType
{
    Normal,
    House
}

public class ObjectSpawner : MonoBehaviour
{
    public float leftPos, rightPos;

    public bool shoudSpawn = true;

    public Transform player;

    public List<GameObject> objects = new();

    public List<GameObject> houses = new();

    public List<GameObject> spawnedObjects = new();

    public Collider bounds;

    //WILL CAUSE AN INFINITE WHILE LOOP IF SET TO 0 AND POSSIBLY AN ERROR IF SET TO LESS
    public float spawnTimer = 1, houseSpawnTimer = 1;

    public Transform parent;

    public float alpha;

    public void Awake()
    {
        if (shoudSpawn)
        {
            StartCoroutine(SpawnObjectsTimer());

            StartCoroutine(SpawnHousesTimer());
        }
    }

    public Vector3 GetRandomPos(SpawningType spawningType)
    {
        if(spawningType == SpawningType.Normal)
        {
            return new Vector3(
                Random.Range(bounds.bounds.min.x, bounds.bounds.max.x),
                Random.Range(bounds.bounds.min.y, bounds.bounds.max.y),
                Random.Range(bounds.bounds.min.z, bounds.bounds.max.z)
            );
        } 

        if(spawningType == SpawningType.House)
        {
            int RandomNum = Random.Range(0, 2);
            float selectedLane = RandomNum == 0 ? leftPos : rightPos;

            return new Vector3(
                Random.Range(bounds.bounds.min.x, bounds.bounds.max.x),
                20,
                selectedLane
                );
        }
        else { return Vector3.zero; }
    }

    IEnumerator SpawnObjectsTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTimer);

            Vector3 pos = GetRandomPos(SpawningType.Normal);

            GameObject obj = Instantiate(objects[Random.Range(0, objects.Count - 1)]);

            obj.transform.position = pos;

            obj.transform.position += new Vector3(0, obj.transform.lossyScale.y / 2, 0);

            //obj.GetComponent<SpawnedObjectBehaviour>().CheckCollision();

            obj.transform.parent = parent;

            spawnedObjects.Add(obj);

            spawnedObjects.RemoveAll(_obj => _obj == null);

            //ToList() to prevent enumeration from the actual list.
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

    IEnumerator SpawnHousesTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(houseSpawnTimer);

            Vector3 pos = GetRandomPos(SpawningType.House);

            GameObject obj = Instantiate(houses[Random.Range(0, houses.Count - 1)]);

            obj.transform.position = pos;
            obj.transform.parent = parent;

            obj.GetComponent<HouseBehavior>().alpha = alpha;

            spawnedObjects.Add(obj);

            spawnedObjects.RemoveAll((_obj) => _obj == null);

            foreach (GameObject house in spawnedObjects.ToList())
            {
                if(house.transform.position.x < player.transform.position.x - 20f)
                {
                    spawnedObjects.Remove(house);
                    Destroy(house);
                }
            }
        }
    }
}
