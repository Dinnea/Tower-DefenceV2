using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Batch
{
    public float intervalBetweenEnemies;
    public float intervalBetweenBatches;
}
[System.Serializable]
public class BatchQueue : Batch
{
    public List<EnemySO> enemyQueue;

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
[System.Serializable]
public class BatchOneType : Batch
{
    public EnemySO enemyType;
    public int enemyNumber;
}

[System.Serializable]
public class BatchPercentage : Batch
{
    public EnemySO defaultEnemy;
    public List<EnemySO> enemyTypes;
    [Range (0, 100)]public List<float> enemyChance;
    
}
