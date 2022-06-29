using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundIdle : MonoBehaviour
{
    [SerializeField] GameObject tutorial;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Prince>() != null)
        {
            print("oT");
        }
    }
}
