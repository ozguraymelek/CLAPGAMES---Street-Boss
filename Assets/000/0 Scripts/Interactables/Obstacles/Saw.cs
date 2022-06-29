using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Saw : Obstacle
{
    private void Update()
    {
        SawMovement();
    }

    public override void KnifeMovement()
    {
        
    }

    public override void SawMovement()
    {
        transform.GetChild(0).Rotate(new Vector3(0f, 1f, 0f));
    }
}
