using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnStrategy : MonoBehaviour, IEnemySpawner
{
    [SerializeField] GameObject _toSpawn;
    [SerializeField] Transform _spawner;
    public void Spawn(EnemySO toSpawn)
    {
        Instantiate(_toSpawn, _spawner.position, _spawner.rotation);
    }
}