using Personal.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider _healthSlider;
    private void Awake()
    {
        _healthSlider = Search.FindComponentInChildrenWithTag<Slider>(gameObject, "HealthBar");
    }
    public void SetMaxHealth(float value, bool resetHealth)
    {
        _healthSlider.maxValue = value;
        if (resetHealth) _healthSlider.value = value;
    }
    public void SetHealth(float value)
    {
        _healthSlider.value = value;
    }

    public void TakeDamage(float value)
    {
        _healthSlider.value -= value;
    }

    public void GetHealed(float value)
    {
        _healthSlider.value += value;
    }
}
