using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HQ : MonoBehaviour, IAttackable
{
    [SerializeField] DamageCalculationStrategy _dmgStrategy;
    [SerializeField] float _maxHealth = 100;
    float _health;
    public Action<float> onTakeDamage { get; set; }
    public static Action onDie;

    //on ememy hurt event => observer, => health bar

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void TakeDmg(float dmg)
    {
        _health -= dmg;
        if(_health< 0) _health = 0;
        onTakeDamage?.Invoke(_health);
        if (_health <= 0)
        {
            Die();
        }
    }

    public void GetHealed(float heal)
    {
        _health += heal;
        if (_health > _maxHealth) _health = _maxHealth;
        onTakeDamage?.Invoke(_health);
    }

    public void Die()
    {
        onDie?.Invoke();
    }

    public float GetMaxHealth()
    {
        return _maxHealth;
    }

    public float GetHealth()
    {
        return _health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IEnemy enemy = other.GetComponent<IEnemy>();
            _dmgStrategy.CalculateDmg(enemy.GetDmg(), this);
            enemy.Die(false);
        }
    }
}
