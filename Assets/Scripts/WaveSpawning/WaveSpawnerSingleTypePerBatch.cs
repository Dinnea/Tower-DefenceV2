using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerV2 : MonoBehaviour, IWaveSpawnStrategy
{
    [SerializeField] List<Wave> _waves;
    IEnemySpawner _enemySpawner;
    [SerializeField] GameObject _spawnContainer;
    Wave _currentWave;
    int _waveIndex = 0;
    private void Awake()
    {
        _enemySpawner = _spawnContainer.GetComponent<IEnemySpawner>();
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

    IEnumerator spawnBatch(Batch batch)
    {
        BatchOneType batchOneType = (batch as BatchOneType);
        for (int i = 0; i< batchOneType.enemyNumber; i++)
        {
            _enemySpawner.Spawn(batchOneType.enemyType);
            yield return new WaitForSeconds(batch.intervalBetweenEnemies);
        }
    }

    IEnumerator spawnWave()
    {
        foreach (Batch batch in _currentWave.batches)
        {
            StartCoroutine(spawnBatch(batch as BatchOneType));
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
