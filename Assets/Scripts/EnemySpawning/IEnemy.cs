using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy 
{
    public void SetHealth(float health);
    public void SetSpeed(float speed);
    public void SetMoney(float money);
    public void Move();
    public void Die();
}
