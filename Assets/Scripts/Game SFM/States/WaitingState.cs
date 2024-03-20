using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingState : GameState
{
    float _enemyCheckTimer = 1;

    public static Action onWaveEnded;

    public override void Handle()
    {
        
        if(_enemyCheckTimer> 0)
        {
            _enemyCheckTimer -= Time.deltaTime;
        }
        else
        {
            if (areAllEnemiesDead())
            {
                levelSFM.ChangeState(GetComponent<BuilderState>());
            }
            _enemyCheckTimer = 1;
        }
    }

    public override void OnEnterState()
    {
        
    }

    public override void OnExitState()
    {
        onWaveEnded?.Invoke();
    }

    bool areAllEnemiesDead()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length == 0;
    }
}
