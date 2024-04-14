using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DefaultDMGCalcStrategy", menuName = "Damage Calculating Strategies/Default")]
public class DefaultDMGStrategy : DamageCalculationStrategy
{
    public override void CalculateDmg(float rawDmg, IAttackable target)
    {
        
        target.TakeDmg(rawDmg);
    }
}
