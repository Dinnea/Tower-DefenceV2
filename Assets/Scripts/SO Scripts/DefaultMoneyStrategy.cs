using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Default Money Strategy", menuName = "Money Strategies/Default Strategy")]
public class DefaultMoneyStrategy : MoneyStrategy
{
    public override float GetMoneyFromEnemy(IEnemy enemy)
    {
        return enemy.GetMoney();
    } 
}
