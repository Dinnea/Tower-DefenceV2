using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable 
{
    public Action<float> onTakeDamage { get; set; }

    public float GetMaxHealth();
    public float GetHealth();

    public void TakeDmg(float damage);
    //public void Die(bool killed = true);
}
