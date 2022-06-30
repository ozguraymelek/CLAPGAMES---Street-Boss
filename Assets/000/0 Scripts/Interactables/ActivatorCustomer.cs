using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ActivatorCustomer : MonoBehaviour
{
    [Header("Scriptable Objects Reference")]
    [Space]
    [SerializeField] private GameSettings gameSettings;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ICustomer>() != null)
        {
            if (StackManager.Instance.objectsOnDesk.Count == 0) return;

            print("Customer interacted!");
            StackManager.Instance.GetAllFoods(other.GetComponent<Customer>(), FindObjectOfType<ActivatorMoney>(), gameSettings);
        }
    }
}
