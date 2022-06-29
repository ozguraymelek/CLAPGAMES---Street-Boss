using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIElementBoing : MonoBehaviour
{
    private void Start()
    {
        transform.DOScale(1.1f * Vector3.one, 1.5f).SetLoops(-1, LoopType.Yoyo);
    }
}