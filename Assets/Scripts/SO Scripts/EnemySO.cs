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

    public GameObject InstantiatePrefab()
    {
        GameObject instance = Instantiate(Resources.Load("EnemyTemplate") as GameObject);
        Transform visualContainer = Search.FindComponentInChildrenWithTag<Transform>(instance, "Visual");
        Instantiate(visual, visualContainer);
        instance.gameObject.name = visual.name;
        return instance;
    }
    public void AddEnemyScript(GameObject gameObject)
    {
        if (gameObject.GetComponent<IEnemy>()==null)
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
