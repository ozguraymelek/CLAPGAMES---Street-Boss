using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Clouds : MonoBehaviour
{
    [SerializeField] Transform[] clouds;
    [SerializeField] Vector3[] cloudFirstPoss;
    [SerializeField] Transform[] targets;

    private void Start()
    {
        GameStarted();
    }

    private void GameStarted()
    {
        float openTime = 1.5f;

        cloudFirstPoss = new Vector3[clouds.Length];

        for (int i = 0; i < clouds.Length; i++)
        {
            clouds[i].gameObject.SetActive(true);
            cloudFirstPoss[i] = clouds[i].position;

            clouds[i].position = targets[i].position;
            clouds[i].localScale = Vector3.one * 15;

            clouds[i].DOMove(cloudFirstPoss[i], openTime);
            clouds[i].DOScale(Vector3.one * 1, openTime);
        }

        DOVirtual.DelayedCall(openTime, () => ActivateClouds(false));

        //SoundManager.Instance.Cloud();
    }

    private void ActivateClouds(bool b)
    {
        for (int i = 0; i < clouds.Length; i++)
        {
            clouds[i].gameObject.SetActive(b);
        }
    }

    private void OnFlagMoveFinished()
    {
        ActivateClouds(true);

        for (int i = 0; i < clouds.Length; i++)
        {
            clouds[i].DOMove(targets[i].position, 2f);
            clouds[i].DOScale(Vector3.one * 15, 2f);
        }

        //SoundManager.Instance.Cloud();
    }
}