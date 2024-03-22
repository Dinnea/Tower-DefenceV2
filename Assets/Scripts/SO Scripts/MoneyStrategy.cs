using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoneyStrategy : ScriptableObject
{
    public abstract float GetMoneyFromEnemy(IEnemy enemy);
}
