using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorMoney : MonoBehaviour, IActivatorMoney
{
    [Header("Settings")]
    [Space]
    [SerializeField] internal List<Transform> moneyIndexes;
    public static int i = 0;

    void IActivatorMoney.OnEnter()
    {
        
    }

    void IActivatorMoney.OnExit()
    {
        i = 0;
    }

    void IActivatorMoney.OnStay(Prince prince)
    {
        if (Input.GetMouseButton(0))
            return;

        StackManager.Instance.TakeAllMonies(prince);
    }
}
