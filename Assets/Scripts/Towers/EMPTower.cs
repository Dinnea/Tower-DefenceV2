using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPTower : LaserTower
{
    private void Update()
    {
        if (!_isOnCooldown) Execute();
    }

    void Execute()
    {
        onAction?.Invoke();
        foreach( GameObject enemy in enemiesInRange )
        {

        }
    }
}
