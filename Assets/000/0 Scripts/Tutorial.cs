using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : Singleton<Tutorial>
{
    [Header("Components")]
    [Space]
    // [SerializeField] private Transform guideArrowObject;
    [SerializeField] protected List<Transform> targets;

    [Header("Settings")] [Space] public int currentIndex;
    private void Start()
    {
        Globals.onTutorial = true;
    }

    private void Update()
    {
        
    }

    public void TutorialAlgorithm(Vector3 pos)
    {
        transform.parent = null;
        transform.parent = targets[currentIndex];
        transform.localPosition = pos; 
        currentIndex += 1;
    }
}
