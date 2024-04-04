using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Default Money Strategy", menuName = "Money Strategies/Default Strategy")]

public class DefaultMoneyEarningStrategy : MoneyEarningStrategy
{
    public override float GetMoneyFromEnemy(Enemy enemy)
    {
        return enemy.GetMoney();
    } 
}
