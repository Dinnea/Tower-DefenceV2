using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> spawns = new List<GameObject>();
    /// <summary>
    /// Spawns an enemy with parameters specified in toSpawn EnemySO (visuals, stats).
    /// If HealthBar is attached, removes the HealthBar script and adds a new version.
    /// </summary>
    /// <param name="toSpawn"></param>
    public void Spawn(EnemySO toSpawn)
    {
        GameObject spawned = toSpawn.InstantiatePrefab();
        spawns.Add(spawned);
        spawned.transform.position = transform.position;
        spawned.transform.rotation = transform.rotation;
        toSpawn.AddEnemyScript(spawned);
        toSpawn.SetParameters(spawned.GetComponent<Enemy>());

        HealthBar tempHPBar = spawned.GetComponent<HealthBar>();
        if (tempHPBar != null ) Destroy(tempHPBar);
        spawned.AddComponent<HealthBar>();
        
    }
}
