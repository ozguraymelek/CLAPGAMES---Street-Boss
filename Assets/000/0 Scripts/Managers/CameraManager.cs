using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using EZCameraShake;
using System;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera activeCam;
    [SerializeField] internal CinemachineVirtualCamera firstCam, gameCam, successCam, failCam, gameCamIdle;

    private void Start()
    {
        activeCam = firstCam;
        ChangeCamera(firstCam);

        CkyEvents.OnStart += OnStart;
        CkyEvents.OnSuccess += SuccessCam;
        CkyEvents.OnFail += FailCam;
        CkyEvents.OnTransToRunner += OnRunner;
        CkyEvents.OnTransToIdle += OnIdle;
    }

    private void OnRunner()
    {
        ChangeCamera(gameCam);
    }

    private void OnIdle()
    {
        ChangeCamera(gameCamIdle);
    }

    private void OnStart()
    {
        GameCam();
    }

    public void GameCam()
    {
        ChangeCamera(gameCam);
    }

    public void Shakeable()
    {
        CameraShaker.Instance.ShakeOnce(2f, 2f, 0.2f, 2f);

        StartCoroutine(Delay0(activeCam));
    }
    IEnumerator Delay0(CinemachineVirtualCamera v)
    {
        v.Follow = null;
        v.LookAt = null;

        yield return new WaitForSeconds(1f);

        // v.Follow = Player.Instance.transform;
        // v.LookAt = Player.Instance.transform;
    }

    public void SuccessCam()
    {
        // activeCam.Follow = null;
        // activeCam.LookAt = null;

        StartCoroutine(Delay(activeCam));
    }

    IEnumerator Delay(CinemachineVirtualCamera v)
    {
        yield return new WaitForSeconds(0.75f);

        ChangeCamera(successCam);
        // v.Follow = Player.Instance.transform;
        // v.LookAt = Player.Instance.transform;
    }

    public void FailCam()
    {
        ChangeCamera(failCam);
    }

    public void ChangeCamera(CinemachineVirtualCamera x)
    {
        if (activeCam == x) return;


        activeCam.Priority = 0;
        activeCam = x;

        //activeCam.Follow = Player.Instance.transform;
        //activeCam.LookAt = Player.Instance.transform;
        activeCam.Priority = 10;
    }

    private void OnPlayerSuccess()
    {
        SuccessCam();
    }

    private void OnPlayerFail()
    {
        FailCam();
    }
}