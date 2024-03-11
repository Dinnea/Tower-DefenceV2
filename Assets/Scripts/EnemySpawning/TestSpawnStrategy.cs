using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnStrategy : MonoBehaviour, IEnemySpawnStrategy
{
    [SerializeField] GameObject _toSpawn;
    [SerializeField] Transform _spawner;
    public void Spawn()
    {
        Instantiate(_toSpawn, _spawner.position, _spawner.rotation);
    }
}