using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dollar : MonoBehaviour
{
    public BoxCollider boxCollider;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Prince>() != null)
        {
            boxCollider.isTrigger = true;
        }
    }
}
