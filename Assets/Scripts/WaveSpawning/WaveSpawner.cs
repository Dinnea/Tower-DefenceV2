using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour 
{ 
    [SerializeField] List<Wave> _waves;
    Spawner _enemySpawner;
    Wave _currentWave;
    int _waveIndex = 0;
    public static Action onFinishedSpawning;
    public static Action onLastWaveSpawned;
    private void Awake()
    {
        _currentWave = _waves[_waveIndex];
        _enemySpawner = GetComponentInChildren<Spawner>();
    }
    /// <summary>
    /// Sends a finished spawning event. informs
    /// </summary>
    public void OnWaveFinished()
    {
        _waveIndex++;
        if (_waveIndex >= _waves.Count)
        {
            onLastWaveSpawned?.Invoke();
            onFinishedSpawning?.Invoke();
        }
        else
        {
            _currentWave = _waves[_waveIndex];
            onFinishedSpawning?.Invoke();
        }
    }

    public void SpawnWave()
    {
       StartCoroutine(spawnWave());
    }

    IEnumerator spawnBatchOneType(BatchOneType batch)
    {
        for (int i = 0; i< batch.enemyNumber; i++)
        {
            
            _enemySpawner.Spawn(batch.enemyType);
            yield return new WaitForSeconds(batch.intervalBetweenEnemies);
        }
    }
    IEnumerator spawnBatchQueue(BatchQueue batch)
    {
        foreach (EnemySO enemy in batch.enemyQueue)
        {
            _enemySpawner.Spawn(enemy);
            yield return new WaitForSeconds(batch.intervalBetweenEnemies);
        }
    }

    IEnumerator spawnWave()
    {
        foreach (Batch batch in _currentWave.batches)
        {
            if (batch is BatchOneType)
            {
                StartCoroutine(spawnBatchOneType(batch as BatchOneType));
            }
            else if (batch is BatchQueue)
            {
                StartCoroutine(spawnBatchQueue(batch as BatchQueue));
            }
            yield return new WaitForSeconds(batch.intervalBetweenBatches);
        }
       OnWaveFinished();
    }

    public int GetCurrentWaveNr()
    {
        return _waveIndex;
    }

    public int GetMaxWaveNr()
    {
        return _waves.Count;
    }
}
