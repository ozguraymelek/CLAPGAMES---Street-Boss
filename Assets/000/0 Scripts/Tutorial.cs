using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : Singleton<Tutorial>
{
    [Header("Components")]
    [Space]
    [SerializeField] private Transform guideArrowObject;
    [SerializeField] protected List<Transform> targets;

    [Header("Settings")]
    [Space]
    [SerializeField] private int targetIndex;
    [SerializeField] private Vector3 currentTarget;
    [SerializeField] private float stayTime, triggerTime = .2f;
    private int targetCount;
    public bool tutorialStarted = false;

    private void Start()
    {
        targetIndex = 0;

        targetCount = targets.Count;

        Globals.onTutorial = true;

    }
}
