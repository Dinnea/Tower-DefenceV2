using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultDMGStrategy : DamageCalculationStrategy
{
    public override float CalculateDmg(float rawDmg, IAttackable target)
    {
        return rawDmg;
    }
}
