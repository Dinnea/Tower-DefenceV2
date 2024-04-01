using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOETower : Tower
{
    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (!_isOnCooldown) Execute();
    }

    void Execute()
    {
        Debug.Log("yeet");
        onAction?.Invoke();
        foreach (GameObject enemy in enemiesInRange)
        {
            IAttackable target = enemy.GetComponent<IAttackable>();
            _damageStrategy.CalculateDmg(dmg, target);
        }
        _isOnCooldown = true;
    }
}
