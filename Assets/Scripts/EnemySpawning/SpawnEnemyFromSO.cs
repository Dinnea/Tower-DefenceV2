using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SpawnEnemyFromSO : MonoBehaviour, IEnemySpawnStrategy
{
    //[SerializeField] EnemySO _enemySO;
    //public void Spawn(EnemySO enemySO))
    //{
    //    enemySO.FindEnemyScript();
    //    enemySO.SetParameters();
    //    Instantiate(enemySO.prefab, transform.position, transform.rotation);
    //}
    public void Spawn(EnemySO toSpawn)
    {
        //toSpawn.FindEnemyScript();
        //toSpawn.SetParameters();
        GameObject temp = Instantiate(toSpawn.prefab, transform.position, transform.rotation);
        toSpawn.SetParameters(temp.GetComponent<IEnemy>());
    }
}
