
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
    public AudioManagement AudioSystem;
    public AudioClip Laugh;

    public float leftPos, rightPos;

    public bool shoudSpawn = true;

    public Transform player, SpawnedElf;

    public List<GameObject> obstacles = new();

    public List<GameObject> houses = new();

    public List<GameObject> elves = new();

    public List<GameObject> powerups = new();

    public List<GameObject> spawnedObjects = new();

    public Collider bounds;
    //the bounds where elves can spawn (it's the child of Spawn Area's gameobject)
    public Collider elfBounds;

    //WILL CAUSE AN INFINITE WHILE LOOP IF SET TO 0 AND POSSIBLY AN ERROR IF SET TO LESS
    public float obstaclesSpawnTimer = 1, houseSpawnTimer = 1, elvesSpawnMinTimer = 5, elvesSpawnMaxTimer = 17, powerupsTimer = 1;

    public Transform parent;

    public float alpha;

    public void Awake()
    {
        SpawnedElf = null;

        if (shoudSpawn)
        {
            StartCoroutine(SpawnObstaclesTimer());

            StartCoroutine(SpawnHousesTimer());

            StartCoroutine(SpawnElvesTimer());

            StartCoroutine(SpawnPowerupsTimer());
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
        else { return Vector3.zero; } // Else should never be hit but can't be removed
    }

    IEnumerator SpawnObstaclesTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(obstaclesSpawnTimer);

            if (Utility.instance.isGameOver)
                yield break;

            Vector3 pos = GetRandomPos(SpawningType.Normal);

            GameObject obj = Instantiate(obstacles[Random.Range(0, obstacles.Count - 1)]);

            obj.transform.position = pos;

            if(obj.GetComponent<SpawnedObjectBehaviour>().compensateForHeight)
                obj.transform.position += new Vector3(0, obj.transform.lossyScale.y / 2, 0);

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

            if (Utility.instance.isGameOver)
                yield break;

            Vector3 pos = GetRandomPos(SpawningType.House);

            GameObject obj = Instantiate(houses[Random.Range(0, houses.Count - 1)]);

            obj.transform.position = pos;
            obj.transform.parent = parent;

            obj.GetComponent<HouseBehavior>().alpha = alpha;

            spawnedObjects.Add(obj);
        }
    }

    IEnumerator SpawnElvesTimer()
    {
        while (true)
        {
            //i'm restr. C137 = You're what ?
            
            yield return new WaitForSeconds(Random.Range(elvesSpawnMinTimer, elvesSpawnMaxTimer));

            if (Utility.instance.isGameOver)
                yield break;

            Vector3 pos = GetRandomPos(SpawningType.Elf);

            if (SpawnedElf == null)
            {
                //when the elf is spawned, it will laugh
                AudioSystem.PlaySFX(Laugh);

                GameObject obj = Instantiate(elves[Random.Range(0, elves.Count - 1)]);

                obj.GetComponent<ElfBehavior>().playerMovement = player.GetComponentInParent<MovementSystem>();

                obj.transform.position = pos;

                obj.transform.parent = parent;

                SpawnedElf = obj.transform;
            }
        }
    }

    IEnumerator SpawnPowerupsTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerupsTimer);

            if (Utility.instance.isGameOver)
                yield break;

            Vector3 pos = GetRandomPos(SpawningType.Normal);
            
            GameObject obj = Instantiate(powerups[Random.Range(0, elves.Count - 1)]);

            obj.transform.position = pos;

            obj.transform.position += new Vector3(0, obj.transform.lossyScale.y / 2, 0);

            obj.transform.parent = parent;
                
            spawnedObjects.Add(obj);
        }
    }
}
