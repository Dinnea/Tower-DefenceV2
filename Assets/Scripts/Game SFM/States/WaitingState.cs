using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingState : GameState
{
    float _enemyCheckTimer = 1;
    bool _lastWave = false;

    public static Action onWaveEnded;

    /// <summary>
    /// Checks periodically if all enemies have been killed. Transitions to the BuilderState if true.
    /// </summary>
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
                if (_lastWave) levelSFM.ChangeState(GetComponent<GameOverState>());
                else levelSFM.ChangeState(GetComponent<BuilderState>());
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
    private void OnEnable()
    {
        WaveSpawner.onLastWaveSpawned += setLastWave;
    }
    private void OnDisable()
    {
        WaveSpawner.onLastWaveSpawned -= setLastWave;
    }

    private void setLastWave()
    {
        _lastWave = true;
    }
    bool areAllEnemiesDead()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length == 0;
    }
}
