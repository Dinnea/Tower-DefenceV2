using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// Derived classes = various classes that contain different methods of storing enemies to spawn per wave.
/// </summary>
public class Batch
{
    public float intervalBetweenEnemies;
    public float intervalBetweenBatches;
}
/// <summary>
/// List of enemies to spawn.
/// </summary>
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
/// <summary>
/// Spawn enemy of a type n times.
/// </summary>
[System.Serializable]
public class BatchOneType : Batch
{
    public EnemySO enemyType;
    public int enemyNumber;
}

/// <summary>
/// unfinished/untested. chance for spawning various enemies.
/// </summary>
[System.Serializable]
public class BatchPercentage : Batch
{
    public EnemySO defaultEnemy;
    public List<EnemySO> enemyTypes;
    [Range (0, 100)]public List<float> enemyChance;
    
}
