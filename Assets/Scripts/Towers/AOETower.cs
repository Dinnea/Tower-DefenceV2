using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOETower : Tower
{
    [SerializeField] BuffSO _effect = null;
    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (!_isOnCooldown) Execute();
    }

    protected override void Execute()
    {
        base.Execute();
        foreach (GameObject enemy in enemiesInRange)
        {
            applyDamage(enemy);
            if(_effect != null) 
            {
                //apply effect
            }
        }        
    }
}
