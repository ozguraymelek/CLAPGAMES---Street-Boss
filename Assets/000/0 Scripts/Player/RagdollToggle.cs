using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollToggle : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    // [SerializeField] protected Rigidbody Rigidbody;
    // [SerializeField] protected CapsuleCollider capsuleCollider;
    protected Collider[] childrenCollider;
    protected Rigidbody[] childrenRigidbody;
    [SerializeField] GameObject pelvis;

    void Awake()
    {
        // animator = GetComponent<Animator>();
        // Rigidbody = GetComponent<Rigidbody>();
        // capsuleCollider = GetComponent<CapsuleCollider>();

        childrenCollider = pelvis.GetComponentsInChildren<Collider>();
        childrenRigidbody = pelvis.GetComponentsInChildren<Rigidbody>();
        Debug.Log(childrenRigidbody.Length + ", " + transform.parent.name);
    }

    private void Start()
    {
        RagdollActivate(false);
    }

    public void AddMorePower(Transform hitTr)
    {
        Vector3 direction = ((transform.position - hitTr.position).normalized + new Vector3(0, 0.75f, 0)).normalized;

        pelvis.GetComponent<Rigidbody>().AddForce(direction * 30, ForceMode.VelocityChange);
    }

    public void RagdollActivate(bool active)
    {
        //children
        // foreach (var collider in childrenCollider)
        //     collider.enabled = active;
        foreach (var rigidb in childrenRigidbody)
        {
            // rigidb.detectCollisions = active;
            rigidb.isKinematic = !active;
            rigidb.useGravity = active;
        }

        //rest
        animator.enabled = !active;
        // Rigidbody.detectCollisions = !active;
        // Rigidbody.isKinematic = active;
        // capsuleCollider.enabled = !active;

        //script.enabled = !active;
    }
}