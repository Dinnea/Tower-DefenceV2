using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningState : GameState
{
    [SerializeField] WaveSpawner _waveSpawner;
    private void Awake()
    {
    }
    public override void Handle()
    {
    }

    public override void OnEnterState()
    {
        _waveSpawner.SpawnWave();
    }

    public override void OnExitState()
    {
    }

    private void finishedSpawning()
    {
       levelSFM.ChangeState(GetComponent<WaitingState>());
    }

    private void OnEnable()
    {
        WaveSpawner.onFinishedSpawning += finishedSpawning;
    }
}
