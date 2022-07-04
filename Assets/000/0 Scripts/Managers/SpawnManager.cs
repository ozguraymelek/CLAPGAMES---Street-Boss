using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private Transform doors;

    public void SpawnDoors(Vector3 pos, Quaternion rot)
    {
        Transform instance = EZ_PoolManager.Spawn(doors, pos, rot);
    }
}
