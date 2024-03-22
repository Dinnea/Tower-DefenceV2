using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "75% Transaction Strategy", menuName = "Transaction Strategies/75% Strategy")]
public class ThreeQuartersTransactionStrategy : TransactionStrategy
{
    public override float CalculateTransaction(float value)
    {
        return 0.75f * value;
    }
}
