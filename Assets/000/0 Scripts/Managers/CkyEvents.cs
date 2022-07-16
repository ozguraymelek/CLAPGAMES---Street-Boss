using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CkyEvents : MonoBehaviour
{
    public static event Action OnStart, OnUpdate, OnFixedUpdate, OnSuccess, OnFail, OnTransToRunner, OnTransToIdle;
    
    public static event Action<float> OnPlayerDamaged;
    public static event Action OnInteractWithObstacle;

    public static event Action<Vector3> OnTutorial;

    private void Awake()
    {
        Reset();
    }

    public void Reset()
    {
        OnStart = null;
        OnUpdate = null;
        OnFixedUpdate = null;
        OnSuccess = null;
        OnFail = null;
        OnPlayerDamaged = null;
        OnTransToRunner = null;
        OnTransToIdle = null;
        OnInteractWithObstacle = null;
        OnTutorial = null;
    }

    public void OnStartButtonClicked()
    {
        OnStart?.Invoke();
    }

    public void Update()
    {
        OnUpdate?.Invoke();
    }

    public void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
    }

    public void OnPlayerSuccess()
    {
        OnSuccess?.Invoke();
    }

    public void OnTutorialActive(Vector3 pos)
    {
        OnTutorial?.Invoke(pos);
    }
    public void OnPlayerFail()
    {
        OnFail?.Invoke();
    }

    public void OnTransitionToRunner()
    {
        OnTransToRunner?.Invoke();
    }

    public void  OnTransitionToIdle()
    {
        OnTransToIdle?.Invoke();
    }

    public void OnInteractWithObstcleObject()
    {
        OnInteractWithObstacle?.Invoke();
    }

    public void OnPlayerHealthChange(float healthSliderValue)
    {
        OnPlayerDamaged?.Invoke(healthSliderValue);
    }

}