using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Knife : Obstacle
{
    [Header("Components")]
    [Space]
    [SerializeField] private Transform column;

    void Start()
    {
        KnifeMovement();
    }

    public override void KnifeMovement()
    {
        column.DOLocalRotate(new Vector3(0f, 0f, 180f),1f).SetLoops(-1, LoopType.Yoyo);
        column.DOLocalMoveX(-4.1f, 1.2f).SetLoops(-1, LoopType.Yoyo);
    }

    public override void SawMovement()
    {
        
    }

}
