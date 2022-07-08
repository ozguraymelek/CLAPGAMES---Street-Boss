using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Citizen : MonoBehaviour
{
    [Header("Components")] [SerializeField]
    private NavMeshAgent agent;
    
    [Header("Settings")]
    [SerializeField]
    private Vector3[] randPositions = new Vector3[SIZE];
    public const int SIZE = 150;
    public Vector3 currentTargetPos = new Vector3();
    
    
    private void Start()
    {
        for (int i = 0; i < SIZE; i++)
        {
            randPositions[i] = GetRandomPos();
        }
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        if (agent.hasPath == false)
        {
            currentTargetPos = randPositions[Random.Range(0, randPositions.Length - 1)];
            agent.SetDestination(currentTargetPos);
        }
    }
    public Vector3 GetRandomPos()
    {
        var minX = Random.Range(-29f, 29f);
        var minZ = Random.Range(55f, 112f);

        var newVec = new Vector3(minX, transform.position.y, minZ);
        
        return newVec;
    }
}
