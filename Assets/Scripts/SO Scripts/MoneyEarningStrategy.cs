using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoneyEarningStrategy : ScriptableObject
{
    public abstract float GetMoneyFromEnemy(Enemy enemy);
}
