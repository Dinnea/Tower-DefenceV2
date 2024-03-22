using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Default Transaction Strategy", menuName = "Transaction Strategies/Default Strategy")]
public class DefaultTransactionStrategy : TransactionStrategy
{
    public override float CalculateTransaction(float value)
    {
        return value;
    }
}
