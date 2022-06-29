using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapticPlugin;

public class PrinceHealth : MonoBehaviour
{
    [SerializeField] float health = 100;
    private float maxHealth;
    [SerializeField] RagdollToggle ragdollToggle;
    [SerializeField] private CkyEvents ckyEvents;

    private void Start()
    {
        ckyEvents = FindObjectOfType<CkyEvents>();

        maxHealth = health;
    }

    private void GetDamage(int damage)
    {
        if (health == maxHealth)
        {
            CkyEvents.OnUpdate += IncreaseHealthWithTime;
        }

        health -= damage;

        float currentSliderValue = health / maxHealth;
        ckyEvents.OnPlayerHealthChange(currentSliderValue);
    }

    private void Dead()
    {
        health = 0;

        float currentSliderValue = 0;
        ckyEvents.OnPlayerHealthChange(currentSliderValue);

        ragdollToggle.RagdollActivate(true);

        ckyEvents.OnPlayerFail();
        CkyEvents.OnUpdate -= IncreaseHealthWithTime;
    }

    float t = 0;

    private void IncreaseHealthWithTime()
    {
        t += Time.deltaTime * 10;

        if (t >= 0.5f)
        {
            t = 0;
            health++;

            float currentSliderValue = health / maxHealth;
            ckyEvents.OnPlayerHealthChange(currentSliderValue);

            if (health == maxHealth)
            {
                FindObjectOfType<PlayerHealthBar>().Close();
                CkyEvents.OnUpdate -= IncreaseHealthWithTime;
            }
        }
    }
}