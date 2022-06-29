using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class IKLook : MonoBehaviour
{
    Animator animator;
    [SerializeField] float weight = 0;

    public Transform targetTr;
    private bool _active;

    void Start()
    {
        animator = GetComponent<Animator>();
        targetTr = GameManager.Instance.transform;

        //CkyEvents.OnIKOpened += OnIkOpened;
        //CkyEvents.OnIKClosed += OnIkClosed;
    }

    //private void OnIkOpened()
    //{
    //    _active = true;
    //}

    //private void OnIkClosed()
    //{
    //    _active = false;
    //}

    private void OnAnimatorIK(int layerIndex)
    {
        if (_active == false) return;

        animator.SetLookAtWeight(1f * weight, 0.8f * weight, 1.0f * weight, .5f * weight, .5f * weight);

        Vector3 direction = targetTr.position + new Vector3(0, 1.2f, 0) - transform.position;

        Ray lookAtRay = new Ray(transform.position, direction);
        animator.SetLookAtPosition(lookAtRay.GetPoint(25));
    }

    public void OpenIKSlightly()
    {
        //weight = Mathf.Lerp(weight, 1f, Time.fixedDeltaTime);
        DOTween.To(() => weight, x => weight = x, 1, 1f);
    }
    public void CloseIKSlightly()
    {
        //weight = Mathf.Lerp(weight, 0f, Time.fixedDeltaTime);
        DOTween.To(() => weight, x => weight = x, 0, 1f);
    }
}