using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderState : GameState
{
    int _waveCountdown;
    public static Action<int> onClockTick;

    public override void Handle()
    {
        if(_waveCountdown == 0)
        {
            levelSFM.ChangeState(GetComponent<SpawningState>());
        }
    }

    public override void OnEnterState()
    {
        _waveCountdown = levelSFM.GetCountdownBetweenWaves();
        onClockTick?.Invoke(_waveCountdown);
        StartCoroutine(countdownToWave());
    }

    public override void OnExitState()
    {
        StopAllCoroutines();
        onClockTick?.Invoke(0);
    }

    private IEnumerator countdownToWave()
    {
        while (_waveCountdown >= 0)
        {
            onClockTick?.Invoke(_waveCountdown);

            yield return new WaitForSeconds(1f);
            _waveCountdown--;
        }
    }
}
