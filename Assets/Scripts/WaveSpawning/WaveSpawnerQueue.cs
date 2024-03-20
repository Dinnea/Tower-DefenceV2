using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerQueue : MonoBehaviour, IWaveSpawnStrategy
{
    [SerializeField] List<Wave> _waves;
    IEnemySpawnStrategy _enemySpawnStrategy;
    [SerializeField] GameObject _enemySpawner;
    Wave _currentWave;
    int _waveIndex = 0;
    private void Awake()
    {
        _enemySpawnStrategy = _enemySpawner.GetComponent<IEnemySpawnStrategy>();
        _currentWave = _waves[_waveIndex];
    }
    public void OnWaveFinished()
    {
        _waveIndex++;
        if(_waveIndex >= _waves.Count) _waveIndex = 0; //for now reset waves if out of bounds
        _currentWave = _waves[_waveIndex];
        IWaveSpawnStrategy.onFinishedSpawning?.Invoke();
    }

    public void SpawnWave()
    {
        StartCoroutine(spawnWave());
    }


    IEnumerator spawnBatch(BatchQueue batch)
    {
        foreach (EnemySO enemy in batch.enemyQueue)
        {
            _enemySpawnStrategy.Spawn(enemy);
            yield return new WaitForSeconds(batch.intervalBetweenEnemies);
        }
    }

    IEnumerator spawnWave()
    {
        foreach (Batch batch in _currentWave.batches)
        {
            StartCoroutine(spawnBatch(batch as BatchQueue));
            yield return new WaitForSeconds(batch.intervalBetweenBatches);
        }
    }

    public int GetCurrentWaveNr()
    {
        return 1;
    }

    public int GetMaxWaveNr()
    {
        return 1;
    }
}
