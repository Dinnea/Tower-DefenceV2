using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    IAttackable _parent = null;
    TextMeshProUGUI _text;
    float _maxHealth;
    void Awake()
    {
        _parent = GetComponentInParent<IAttackable>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        _maxHealth = _parent.GetMaxHealth();
    }

    private void setHealthText(float value)
    {
        _text.text = value.ToString() + "/" +  _maxHealth;
    }

    private void OnEnable()
    {
        _parent.onTakeDamage += setHealthText;
    }

    private void OnDisable()
    {
        _parent.onTakeDamage -= setHealthText;
    }
}
