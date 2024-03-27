using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageCalculationStrategy : ScriptableObject
{
   public abstract float CalculateDmg(float rawDmg, IAttackable target);
}
