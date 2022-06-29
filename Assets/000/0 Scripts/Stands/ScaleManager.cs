using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ScaleManager : MonoBehaviour
{
    [SerializeField] private float durationXZ;
    [SerializeField] private float durationY;
    private void OnEnable()
    {
        transform.DOScaleX(1f, durationXZ);
        transform.DOScaleZ(1f, durationXZ);
        transform.DOScaleY(1f, durationY);
    }
}
