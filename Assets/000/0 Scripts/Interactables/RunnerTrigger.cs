using System;
using System.Collections;
using System.Collections.Generic;
using EZ_Pooling;
using UnityEngine;

public class RunnerTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private GameSettings gameSettings;

    private void Start()
    {
        // CkyEvents.OnTransToIdle += DeactivateGameInfoPanel;
    }

    void IInteractable.OnEnter()
    {
        FindObjectOfType<CkyEvents>().OnTransitionToRunner();
        Destroy(LevelManager.Instance.activeLevel.gameObject);
        LevelManager.Instance.ActivateStartLevel(gameSettings);
        LevelManager.Instance.RandomLevel();
    }

    void IInteractable.OnExit()
    {

    }

    void IInteractable.OnStay()
    {

    }
    
    public void DeactivateGameInfoPanel()
    {
        UI_Manager.Instance.panelGameInfoCanvas.SetActive(false);
    }
}