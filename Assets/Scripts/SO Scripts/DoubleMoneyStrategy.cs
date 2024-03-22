using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Double Money Strategy", menuName = "Money Strategies/Double Strategy")]
public class DoubleMoneyStrategy : MoneyStrategy
{
    public override float GetMoneyFromEnemy(IEnemy enemy)
    {
        return 2 * enemy.GetMoney();
    } 
}
