using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prince : Singleton<Prince> 
{
    [Header("References")]
    [SerializeField] internal JoystickPlayer player;

    [Header("Settings")]
    [SerializeField] internal Transform playerCollectedMoneyParent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            interactable.OnEnter();
        }
        if(other.TryGetComponent(out IActivatorDesk activatorDesk))
        {
            activatorDesk.OnEnter();
        }
        if(other.TryGetComponent(out IActivatorMoney activatorMoney))
        {
            activatorMoney.OnEnter();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            interactable.OnStay();
        }
        if (other.TryGetComponent(out IActivatorDesk activatorDesk))
        {
            activatorDesk.OnStay();
        }
        if (other.TryGetComponent(out IActivatorMoney activatorMoney))
        {
            activatorMoney.OnStay(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            interactable.OnExit();
        }
        if (other.TryGetComponent(out IActivatorDesk activatorDesk))
        {
            activatorDesk.OnExit();
        }
        if (other.TryGetComponent(out IActivatorMoney activatorMoney))
        {
            activatorMoney.OnExit();
        }
    }
}