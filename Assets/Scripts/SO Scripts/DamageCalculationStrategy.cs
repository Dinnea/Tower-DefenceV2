using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageCalculationStrategy : ScriptableObject
{
   public abstract void CalculateDmg(float rawDmg, IAttackable target);
}
