using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    [Header("References")]
    [Space]
    [SerializeField] private CkyBehaviour behaviour;

    private void Start()
    {
        behaviour.ObstacleMovementKnife(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        IStackable _interactable = other.GetComponent<IStackable>();

        if (_interactable == null) return;

        _interactable.Scatter();

        FindObjectOfType<CkyEvents>().OnInteractWithObstcleObject();

        Destroy(gameObject);
    }
    public abstract void KnifeMovement();
    public abstract void SawMovement();
}