using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorDesk : Singleton<ActivatorDesk>, IActivatorDesk
{
    [Header("Settings")]
    [Space]
    private float _stayedTime;
    public static int İndexDesk = 0;
    public List<Transform> foodStackPoints;

    void IActivatorDesk.OnEnter()
    {
        
    }

    void IActivatorDesk.OnExit()
    {
        StackManager.Instance.posToStackY = 0;
    }

    void IActivatorDesk.OnStay()
    {
        if (Input.GetMouseButton(0))
            return;

        StackManager.Instance.StackToDesk();
    }
}
