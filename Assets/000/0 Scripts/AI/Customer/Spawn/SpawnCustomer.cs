using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnCustomer : MonoBehaviour
{
    [SerializeField] private List<Customer> customers;
    [SerializeField] private Customer customer;
    [FormerlySerializedAs("spawnTime")] public float timer = 0;
    public float spawnRate;
    
    private void Update()
    {
        if (StackManager.Instance.objectsOnDesk.Count != 0)
        {
            timer += Time.deltaTime;
        
            if (timer >= spawnRate)
            {
                Customer cust = Instantiate(customer, transform.position, Quaternion.identity);
                timer = 0;
            }
        }
        else
        {
            timer = 0;
        }
        
    }
}
