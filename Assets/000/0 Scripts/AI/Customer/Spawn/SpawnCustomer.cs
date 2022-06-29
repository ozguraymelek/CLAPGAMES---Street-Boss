using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCustomer : MonoBehaviour
{
    [SerializeField] private List<Customer> customers;
    [SerializeField] private Customer customer;
    public float spawnTime = 0;
    
    private void Update()
    {
        spawnTime += Time.deltaTime;

        if (spawnTime >= 5f)
        {
            Customer cust = Instantiate(customer, transform.position, Quaternion.identity);
            spawnTime = 0;
        }
    }
}
