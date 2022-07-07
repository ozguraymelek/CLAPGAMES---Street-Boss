using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Saw : Obstacle
{
    private void Start()
    {
        behaviour = FindObjectOfType<CkyBehaviour>();
    }

    private void Update()
    {
        SawMovement();
    }

    public override void KnifeMovement()
    {
        
    }

    public override void SawMovement()
    {
        transform.eulerAngles += new Vector3(1f, 0f, 0f);
    }
}
