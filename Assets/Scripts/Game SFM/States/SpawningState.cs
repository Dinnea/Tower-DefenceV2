using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningState : GameState
{
    [SerializeField] GameObject _waveSpawner;
    IWaveSpawnStrategy _waveSpawnStrategy;
    private void Awake()
    {
        _waveSpawnStrategy = _waveSpawner.GetComponent<IWaveSpawnStrategy>();
    }
    public override void Handle()
    {
        
    }

    public override void OnEnterState()
    {
       
        _waveSpawnStrategy.SpawnWave();
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
        IWaveSpawnStrategy.onFinishedSpawning += finishedSpawning;
    }
}
