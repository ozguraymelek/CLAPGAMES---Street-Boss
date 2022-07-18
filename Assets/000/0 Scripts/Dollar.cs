using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dollar : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Prince>() != null)
        {
            other.collider.isTrigger = true;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.GetComponent<Prince>() != null)
        {
            other.collider.isTrigger = false;
        }
    }
}
