using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy 
{
    //get world loc
    public void SetHealth(float health);
    public void SetSpeed(float speed);
    public void SetMoney(float money);
    public float GetMoney();
    public void Move();
    public void Die();

    public Vector3 GetWorldLocation();
    void SetDmg(float dmg);
    float GetDmg();
}
