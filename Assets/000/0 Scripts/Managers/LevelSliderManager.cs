using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSliderManager : Singleton<LevelSliderManager>
{
    [SerializeField] Slider levelSlider;

    private void Start()
    {
        levelSlider.value = 0;
    }

    public void UpdateLevelSlider(float sliderValue)
    {
        levelSlider.value = sliderValue;
    }
}