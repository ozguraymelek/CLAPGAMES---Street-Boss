using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Canvas healthBarCanvas;
    private Quaternion _canvasRotation;
    [SerializeField] Slider healthSlider;
    private Transform princeTr;
    private Camera cam;
    private bool _isOpen;

    private void Start()
    {
        _canvasRotation = healthSlider.transform.rotation;
        healthSlider.gameObject.SetActive(false);

        CkyEvents.OnPlayerDamaged += Open;
        CkyEvents.OnFail += Close;
    }

    private void Update()
    {
        if (_isOpen == false) return;

        healthSlider.transform.rotation = _canvasRotation;
    }

    private void Open(float healthSliderValue)
    {
        _isOpen = true;

        if (healthSlider == null) return;

        healthSlider.gameObject.SetActive(true);
        SliderUpdate(healthSliderValue);
    }

    private void SliderUpdate(float healthSliderValue)
    {
        healthSlider.value = healthSliderValue;
    }

    public void Close()
    {
        _isOpen = false;

        healthSlider.gameObject.SetActive(false);
    }
}