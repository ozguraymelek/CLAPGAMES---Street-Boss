using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCrosshair : MonoBehaviour
{
    [SerializeField] private Transform gun;
    private Transform _crosshairTr;
    private Camera _thisCam;

    private void Start()
    {
        _thisCam = GetComponent<Camera>();
        _crosshairTr = FindObjectOfType<CkyBehaviour>().transform;
    }

    private void FixedUpdate()
    {
        Ray rayOrigin = _thisCam.ScreenPointToRay(_crosshairTr.position);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            if (hitInfo.collider != null)
            {
                Vector3 direction = hitInfo.point - gun.position;
                gun.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}