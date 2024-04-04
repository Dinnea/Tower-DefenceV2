using Personal.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName ="ScriptableObjects/Enemies")]
public class EnemySO : ScriptableObject
{
    public GameObject visual;
    public string enemyType;
    public float health;
    public float speed;
    public float money;
    public float dmg;

    /// <summary>
    /// Spawns a new enemy, adds the visual, sets the name.
    /// </summary>
    /// <returns>Returns the spawned enemy.</returns>
    public GameObject InstantiatePrefab()
    {
        GameObject instance = Instantiate(Resources.Load("EnemyTemplate") as GameObject);
        Transform visualContainer = Search.FindComponentInChildrenWithTag<Transform>(instance, "Visual");
        Instantiate(visual, visualContainer);
        instance.gameObject.name = visual.name;
        return instance;
    }
    /// <summary>
    /// If the enemy prefab does not yet have an Enemy script, add a script determined by the enemyType string. BasicEnemy => default.
    /// </summary>
    /// <param name="gameObject"></param>
    public void AddEnemyScript(GameObject gameObject)
    {
        if (gameObject.GetComponent<Enemy>()==null)
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
    /// <summary>
    /// Sets parameters of an abstract Enemy class.
    /// </summary>
    /// <param name="tower"></param>
    public void SetParameters(Enemy enemyScript)
    {
        enemyScript.SetHealth(health);
        enemyScript.SetSpeed(speed);
        enemyScript.SetMoney(money);
        enemyScript.SetDmg(dmg);
    }
}
