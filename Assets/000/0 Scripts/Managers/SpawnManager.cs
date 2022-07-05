using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private List<GameObject> levels;

    public Transform SpawnLevels(Vector3 pos, Quaternion rot,int index)
    {
        Transform instance = Instantiate(levels[index].transform, pos, rot);
        return instance;
    }
}
