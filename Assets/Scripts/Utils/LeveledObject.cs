using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LeveledObject", menuName = "ScriptableObjects/LeveledObject")]
public class LeveledObject : ScriptableObject
{
    public GameObject prefab;

    public int levelNeeded;
}
