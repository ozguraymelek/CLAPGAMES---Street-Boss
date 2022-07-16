using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoingEffect : MonoBehaviour
{
    private Transform camTr;
    [SerializeField] float t = 1.2f;

    private void Start()
    {
        camTr = Camera.main.transform;

        transform.DOScaleY(1.2f, 1).SetLoops(-1, LoopType.Yoyo);
        transform.DOMoveY(3, 1).SetLoops(-1, LoopType.Yoyo);
    }
    private void Update()
    {
        transform.Rotate(new Vector3(0f, 25f*Time.deltaTime, 0f));
    }
}