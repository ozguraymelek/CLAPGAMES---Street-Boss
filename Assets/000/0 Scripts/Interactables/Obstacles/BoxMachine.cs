using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMachine : Obstacle
{
    private void Start()
    {
        behaviour = FindObjectOfType<CkyBehaviour>();
    }
    public override void KnifeMovement()
    {
        
    }

    public override void SawMovement()
    {
        
    }
}
