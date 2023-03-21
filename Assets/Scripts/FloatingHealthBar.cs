using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tactical;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    public Health healthComponent;
    public Slider healthSlider;

    private void Start()
    {
        healthSlider.maxValue = healthComponent.maxHealth;
        healthSlider.value = healthComponent.currentHealth;
    }

    public void OnHealthChange(int current, int delta)
    {
        healthSlider.value = current;
    }
}