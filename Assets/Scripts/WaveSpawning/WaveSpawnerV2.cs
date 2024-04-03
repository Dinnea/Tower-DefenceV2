using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerV2 : MonoBehaviour 
{ 
    [SerializeField] List<Wave> _waves;
    IEnemySpawner _enemySpawner;
    Wave _currentWave;
    int _waveIndex = 0;
    public static Action onFinishedSpawning;
    private void Awake()
    {
        Debug.Log(_enemySpawner = GetComponentInChildren<IEnemySpawner>());
        _currentWave = _waves[_waveIndex];
    }
    public void OnWaveFinished()
    {
        _waveIndex++;
        if(_waveIndex >= _waves.Count) _waveIndex = 0; //for now reset waves if out of bounds
        _currentWave = _waves[_waveIndex];
        onFinishedSpawning?.Invoke();
    }

    public void SpawnWave()
    {
       StartCoroutine(spawnWave());
    }

    IEnumerator spawnBatchOneType(Batch batch)
    {
        BatchOneType batchOneType = batch as BatchOneType;
        for (int i = 0; i< batchOneType.enemyNumber; i++)
        {
            
            _enemySpawner.Spawn(batchOneType.enemyType);
            yield return new WaitForSeconds(batch.intervalBetweenEnemies);
        }
    }
    IEnumerator spawnBatchQueue(BatchQueue batch)
    {
        foreach (EnemySO enemy in batch.enemyQueue)
        {
            //_enemySpawner.Spawn(enemy);
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
