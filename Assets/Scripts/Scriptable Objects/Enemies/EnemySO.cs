using Personal.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName ="ScriptableObjects/Enemies")]
public class EnemySO : ScriptableObject
{
    public GameObject prefab;
    IEnemy enemyScript;
    public float health;
    public float speed;
    public float money;

    public void FindEnemyScript()
    {
        enemyScript = prefab.GetComponent<IEnemy>();
    }
    public void SetParameters()
    {
        enemyScript.SetHealth(health);
        enemyScript.SetSpeed(speed);
        enemyScript.SetMoney(money);
    }
}
