
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Jobs;
using Unity.Collections;
using UnityEngine;

public enum SpawningType
{
    Normal,
    House,
    Elf
}

public class ObjectSpawner : MonoBehaviour
{
    public float leftPos, rightPos;

    public bool shoudSpawn = true;

    public Transform player, SpawnedElf;

    public List<GameObject> objects = new();

    public List<GameObject> houses = new();

    public List<GameObject> elves = new();

    public List<GameObject> spawnedObjects = new();

    public Collider bounds;
    //the bounds where elves can spawn (it's the child of Spawn Area's gameobject)
    public Collider elfBounds;

    //WILL CAUSE AN INFINITE WHILE LOOP IF SET TO 0 AND POSSIBLY AN ERROR IF SET TO LESS
    public float spawnTimer = 1, houseSpawnTimer = 1, elvesSpawnTimer = 1;

    public Transform parent;

    public float alpha;

    public void Awake()
    {
        SpawnedElf = null;

        if (shoudSpawn)
        {
            StartCoroutine(SpawnObjectsTimer());

            StartCoroutine(SpawnHousesTimer());

            StartCoroutine(SpawnElvesTimer());
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

        if(spawningType == SpawningType.Elf)
        {
            //random position in the elfbounds area
            return new Vector3(
                Random.Range(elfBounds.bounds.min.x, elfBounds.bounds.max.x),
                5,
                Random.Range(elfBounds.bounds.min.z, elfBounds.bounds.max.z)
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

    IEnumerator SpawnElvesTimer()
    {
        while (true)
        {
            //i'm restr
            
                yield return new WaitForSeconds(elvesSpawnTimer);

                Vector3 pos = GetRandomPos(SpawningType.Elf);
            if (SpawnedElf == null)
            {
                GameObject obj = Instantiate(elves[Random.Range(0, elves.Count - 1)]);

                obj.GetComponent<ElfBehavior>().playerMovement = player.GetComponentInParent<MovementSystem>();

                obj.transform.position = pos;

                obj.transform.parent = parent;

                SpawnedElf = obj.transform;
            } else
            {
                print("X");
            }
        }
    }
}
