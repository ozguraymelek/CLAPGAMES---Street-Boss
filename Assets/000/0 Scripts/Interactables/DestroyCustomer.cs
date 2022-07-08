using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCustomer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ICustomer>() != null)
        {
            Destroy(other.gameObject);
        }
    }
}
