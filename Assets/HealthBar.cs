using Personal.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider _healthSlider;
    IAttackable _parent = null;
    private void Awake()
    {
        _healthSlider = Search.FindComponentInChildrenWithTag<Slider>(gameObject, "HealthBar");
        _parent = GetComponentInParent<IAttackable>();
        //setMaxHealth(_parent.GetMaxHealth(), true);
    }
    private void Start()
    {
        setMaxHealth(_parent.GetMaxHealth(), true);
        _parent.onTakeDamage += setHealth;
    }
    void setMaxHealth(float value, bool resetHealth)
    {
        _healthSlider.maxValue = value;
        if (resetHealth) _healthSlider.value = value;
    }
    void setHealth(float value)
    {
        _healthSlider.value = value;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        _parent.onTakeDamage -= setHealth;
    }
}
