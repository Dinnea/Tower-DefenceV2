using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerQueue : MonoBehaviour, IWaveSpawnStrategy
{
    [SerializeField] List<Wave> _waves;
    IEnemySpawnStrategy _enemySpawnStrategy;
    [SerializeField] GameObject _enemySpawner;
    Wave _currentWave;
    Batch _currentBatch;
    private void Awake()
    {
        _enemySpawnStrategy = _enemySpawner.GetComponent<IEnemySpawnStrategy>();
    }
    public void OnWaveFinished()
    {
        IWaveSpawnStrategy.onFinishedSpawning?.Invoke();
    }

    public void SpawnWave()
    {
        _currentWave = _waves[0];
        StartCoroutine(spawnWave());
    }


    IEnumerator spawnBatch(Batch batch)
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
            StartCoroutine(spawnBatch(batch));
            yield return new WaitForSeconds(batch.intervalBetweenBatches);
        }
    }

}
