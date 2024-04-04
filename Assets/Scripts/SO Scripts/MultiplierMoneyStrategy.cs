using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Double Money Strategy", menuName = "Money Strategies/Double Strategy")]
public class MultiplierMoneyStrategy : MoneyEarningStrategy
{
    public float modifier; 
    public override float GetMoneyFromEnemy(Enemy enemy)
    {
        return modifier * enemy.GetMoney();
    } 
}
