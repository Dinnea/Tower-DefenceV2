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
    [SerializeField] bool _loopWaves;
    private void Awake()
    {
        _currentWave = _waves[_waveIndex];
        _enemySpawner = GetComponentInChildren<Spawner>();
        //SpawnWave();
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
            if (_loopWaves) 
            { 
                _waveIndex = 0; 
                Debug.Log("Looped waves"); 
            }
            else onFinishedSpawning?.Invoke();
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
                yield return spawnBatchOneType(batch as BatchOneType);
            }
            else if (batch is BatchQueue)
            {
                yield return spawnBatchQueue(batch as BatchQueue);//StartCoroutine(spawnBatchQueue(batch as BatchQueue));
            }
            yield return new WaitForSeconds(0.5f);
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

    public Wave GetCurrentWave()
    {
        return _currentWave;
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
    }
}
