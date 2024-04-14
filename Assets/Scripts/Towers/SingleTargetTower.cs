using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventBus<Event>;

public class SingleTargetTower : Tower
{
    [SerializeField] GameObject _target = null;

    private void Start()
    {
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
        selectTarget();

        if (_target != null && !_isOnCooldown) Execute();
    }

    protected override void OnAwake()
    {
        base.OnAwake();

    }

    protected override void Execute()
    {
        applyDamage(_target);
        base.Execute();
        
    }

    /// <summary>
    /// Logic for selecting the tower's primary target. Will select a new one if current is out of range.
    /// </summary>
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
    public GameObject GetTarget()
    {
        return _target;
    }
}
