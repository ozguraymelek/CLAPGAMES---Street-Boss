using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private GameSettings gameSettings;
    void IInteractable.OnEnter()
    {
        FindObjectOfType<CkyEvents>().OnTransitionToRunner();
        LevelManager.Instance.ActivateNextLevel(gameSettings);
    }

    void IInteractable.OnExit()
    {

    }

    void IInteractable.OnStay()
    {

    }
}