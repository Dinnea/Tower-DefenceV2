using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SpawnEnemyFromSO : MonoBehaviour, IEnemySpawner
{
    public void Spawn(EnemySO toSpawn)
    {
        GameObject spawned = Instantiate(toSpawn.prefab, transform.position, transform.rotation); 
        toSpawn.AddEnemyScript(spawned);
        toSpawn.SetParameters(spawned.GetComponent<IEnemy>());
    }
}
