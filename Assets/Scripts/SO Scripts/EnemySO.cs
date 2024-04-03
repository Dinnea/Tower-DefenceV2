using Personal.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName ="ScriptableObjects/Enemies")]
public class EnemySO : ScriptableObject
{
    public GameObject prefab;
    public string enemyType;
    public float health;
    public float speed;
    public float money;
    public float dmg;

    public void AddEnemyScript(GameObject gameObject)
    {
        if (prefab.GetComponent<IEnemy>()==null)
        {
            switch (enemyType)
            {
                case "basic":
                    gameObject.AddComponent<BasicEnemy>();
                    break;
                default:
                    gameObject.AddComponent<BasicEnemy>();
                    break;
            }
        }
    }
    public void SetParameters(IEnemy enemyScript)
    {
        enemyScript.SetHealth(health);
        enemyScript.SetSpeed(speed);
        enemyScript.SetMoney(money);
        enemyScript.SetDmg(dmg);
    }
}
