using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave 
{
    public List<Batch> batches;

    public Batch GetBatch()
    {
        return batches[0];
    }
}

[System.Serializable]
public class Batch
{
    public List<EnemySO> enemyQueue;
    public float intervalBetweenEnemies;
    public float intervalBetweenBatches;

    int _queueIndex = 0;
    public int GetQueueIndex()
    {
        return _queueIndex;
    }
    public void IncrementQueueIndex()
    {
        _queueIndex++;
    }

    public EnemySO GetCurrentEnemy()
    {
        EnemySO temp = enemyQueue[_queueIndex];
        _queueIndex++;
        return temp;
    }
}
