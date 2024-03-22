using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TransactionStrategy : ScriptableObject
{
    public abstract float CalculateTransaction(float value);
}
