using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorCustomer : MonoBehaviour
{
    [Header("Scriptable Objects Reference")]
    [Space]
    [SerializeField] private GameSettings gameSettings;

    [Header("Settings")]
    [Space]
    [SerializeField] internal List<Transform> moneyIndexes;
    public static int i = 0;
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
