using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventBus<Event>;

public class LaserTower : Tower
{
    [SerializeField] GameObject _target = null;

    protected override void OnUpdate()
    {
        base.OnUpdate();
        selectTarget();

        if (_target != null && !_isOnCooldown) Execute();
    }

    private void Execute()
    {
        onAction?.Invoke();

        _isOnCooldown = true;
        IAttackable target = _target.GetComponent<IAttackable>();
        _damageStrategy.CalculateDmg(dmg, target);
    }

    void selectTarget()
    {
        if (enemiesInRange.Count > 0)
        {
           _target = enemiesInRange[0];
        }
        else _target = null;
    }
    public Transform GetTargetLocation()
    {
        if (_target != null) return _target.transform;
        return null;
    }
}
