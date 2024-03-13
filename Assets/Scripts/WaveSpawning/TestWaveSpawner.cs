using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWaveSpawner : MonoBehaviour, IWaveSpawnStrategy
{
    [SerializeField] GameObject _enemySpawner;
    IEnemySpawnStrategy _enemySpawnStrategy;
    public List<Wave> _waves;

    private void Awake()
    {
        _enemySpawnStrategy = _enemySpawner.GetComponent<IEnemySpawnStrategy>();
    }
    public void SpawnWave()
    {
        //_enemySpawnStrategy.Spawn();
        OnWaveFinished();
    }
    public void OnWaveFinished()
    {
        Debug.Log("Switch to waiting");
        IWaveSpawnStrategy.onFinishedSpawning?.Invoke();
    }
}
