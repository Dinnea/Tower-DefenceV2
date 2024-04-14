using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.AI;
using static UnityEngine.ParticleSystem;
using UnityEditor.Sprites;

public class TowerTests
{
    WaveSpawner _spawner;
    BuildingTypeSO[] _towers;
    GameObject[] _navPoints;

    //OneTimeSetup is a NUnit attribute, it identify methods that are called once
    //prior to executing any of the tests.
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("TowerTestScene");
    }
    [UnitySetUp]
    public IEnumerator SetupTests()
    {
        //Wait for the scene to finish loading. 
        yield return new WaitForSeconds(0.5f);

        //Then setup the tests.
        _spawner = GameObject.FindAnyObjectByType<WaveSpawner>();
        _towers = GameObject.FindObjectOfType<TowerList>()._buildings;
        _navPoints = GameObject.FindGameObjectsWithTag("NavPoint");

    }

    [UnityTest] 
    public IEnumerator TargetInRange()
    {
        WaveSpawner testWaveSpawner = GameObject.Instantiate<WaveSpawner>(_spawner);
        BuildingTypeSO type = _towers[1];
        GameObject towerObject = GameObject.Instantiate(type.prefab, Vector3.zero, Quaternion.identity);
        Tower tower = towerObject.GetComponent<Tower>();
        type.SetParameters(tower);
        tower.SetDMG(1); //make sure to not kill 

        testWaveSpawner.SpawnWave();
        yield return new WaitForSeconds(testWaveSpawner.GetCurrentWave().batches[0].intervalBetweenEnemies * 2f);
        testWaveSpawner.StopSpawning();
        Spawner testSpawner = testWaveSpawner.gameObject.GetComponentInChildren<Spawner>();
        Enemy enemy = testSpawner.spawns[0].GetComponent<Enemy>();
        enemy.autoNavigate = false;
        yield return new WaitForEndOfFrame();
        float hpBeforeAttack = enemy.GetHealth();
        foreach (GameObject nav in _navPoints)
        {
            if (nav.GetComponent<NavPoint>().index == 1) enemy.transform.position = nav.GetComponent<NavPoint>().GetNavLocation();
        }
        
        yield return new WaitForSeconds(type.attackRate); //after attack
        //check health
        float hpAfterAttack = enemy.GetHealth();
        Assert.AreEqual(hpBeforeAttack - hpAfterAttack, 1);
        //check target
        Assert.IsNotNull(towerObject.GetComponent<SingleTargetTower>().GetTarget());
        Assert.AreEqual(towerObject.GetComponent<SingleTargetTower>().GetTarget(), enemy.gameObject);
    }
    [UnityTest]
    public IEnumerator SelectTargetNotInRange()
    {
        WaveSpawner testWaveSpawner = GameObject.Instantiate<WaveSpawner>(_spawner);
        BuildingTypeSO type = _towers[1];
        GameObject towerObject = GameObject.Instantiate(type.prefab, Vector3.zero, Quaternion.identity);
        Tower tower = towerObject.GetComponent<Tower>();
        type.SetParameters(tower);
        tower.SetDMG(0);

        testWaveSpawner.SpawnWave();
        yield return new WaitForSeconds(testWaveSpawner.GetCurrentWave().batches[0].intervalBetweenEnemies * 2f);
        testWaveSpawner.StopSpawning();
        Spawner testSpawner = testWaveSpawner.gameObject.GetComponentInChildren<Spawner>();
        Enemy enemy = testSpawner.spawns[0].GetComponent<Enemy>();
        enemy.autoNavigate = false;
        foreach (GameObject nav in _navPoints)
        {
            if (nav.GetComponent<NavPoint>().index == 0) enemy.Move(nav.GetComponent<NavPoint>().GetNavLocation());
        }
        yield return new WaitForSeconds(3f);

        Assert.IsNull(towerObject.GetComponent<SingleTargetTower>().GetTarget());
    }

    [UnityTest]
    public IEnumerator TestReselectTargetInRange()
    {
        WaveSpawner testWaveSpawner = GameObject.Instantiate<WaveSpawner>(_spawner);
        BuildingTypeSO type = _towers[1];
        GameObject towerObject = GameObject.Instantiate(type.prefab, Vector3.zero, Quaternion.identity);
        Tower tower = towerObject.GetComponent<Tower>();
        type.SetParameters(tower);
        tower.SetDMG(0);

        testWaveSpawner.SpawnWave();
        yield return new WaitForSeconds(testWaveSpawner.GetCurrentWave().batches[0].intervalBetweenEnemies * 2f);
        testWaveSpawner.StopSpawning();
        Spawner testSpawner = testWaveSpawner.gameObject.GetComponentInChildren<Spawner>();
        Enemy enemy1 = testSpawner.spawns[0].GetComponent<Enemy>();
        
        yield return new WaitForEndOfFrame();
        foreach (GameObject nav in _navPoints)
        {
            if (nav.GetComponent<NavPoint>().index == 1) enemy1.transform.position = nav.GetComponent<NavPoint>().GetNavLocation();
            enemy1.autoNavigate = false;
        }
        yield return new WaitForSeconds(type.attackRate);
        Assert.IsNotNull(towerObject.GetComponent<SingleTargetTower>().GetTarget());
        Assert.AreEqual(towerObject.GetComponent<SingleTargetTower>().GetTarget(), enemy1.gameObject);
        foreach (GameObject nav in _navPoints)
        {
            if (nav.GetComponent<NavPoint>().index == 0) enemy1.transform.position = nav.GetComponent<NavPoint>().GetNavLocation();
        }
        //towerObject.GetComponent<SingleTargetTower>().ResetTarget();
        yield return new WaitForSeconds(1f);
        Assert.IsTrue(towerObject.GetComponent<SingleTargetTower>().GetTarget() == null);
        //testSpawner.spawns.Clear();

        testWaveSpawner.SpawnWave();
        yield return new WaitForSeconds(testWaveSpawner.GetCurrentWave().batches[0].intervalBetweenEnemies * 2f);
        testWaveSpawner.StopSpawning();
        Enemy enemy2 = testSpawner.spawns[1].GetComponent<Enemy>();
       
        yield return new WaitForEndOfFrame();
        foreach (GameObject nav in _navPoints)
        {
            if (nav.GetComponent<NavPoint>().index == 1) enemy2.transform.position = nav.GetComponent<NavPoint>().GetNavLocation();
            enemy2.SetSpeed(0);
        }
        yield return new WaitForSeconds(2);
        Assert.IsNotNull(towerObject.GetComponent<SingleTargetTower>().GetTarget());
        Assert.AreEqual(towerObject.GetComponent<SingleTargetTower>().GetTarget(), enemy2.gameObject);
    }

    [UnityTest] 
    public IEnumerator DoesAOEHitAll()
    {
        WaveSpawner testWaveSpawner = GameObject.Instantiate<WaveSpawner>(_spawner);
        BuildingTypeSO type = _towers[2];
        GameObject towerObject = GameObject.Instantiate(type.prefab, Vector3.zero, Quaternion.identity);
        Tower tower = towerObject.GetComponent<Tower>();
        type.SetParameters(tower);
        yield return new WaitForEndOfFrame();
        Spawner testSpawner = testWaveSpawner.gameObject.GetComponentInChildren<Spawner>();
        
        for (int i = 0; i<5;  i++)
        {
            testWaveSpawner.SpawnWave();
            yield return new WaitForSeconds(testWaveSpawner.GetCurrentWave().batches[0].intervalBetweenEnemies * 2f);
            testWaveSpawner.StopSpawning();
        }
        float hpAssumption = testSpawner.spawns[0].GetComponent<Enemy>().GetHealth() - type.damage;
        foreach (GameObject go in testSpawner.spawns)
        {
            go.GetComponent<Enemy>().autoNavigate = false;
            go.GetComponent<Enemy>().SetSpeed(0);
            go.transform.position = tower.transform.position;
            
        }

        yield return new WaitForSeconds(type.attackRate);
        foreach (GameObject go in testSpawner.spawns)
        {
            Assert.AreEqual(go.GetComponent<Enemy>().GetHealth(), hpAssumption);
        }
    }
    [UnityTest]

    public IEnumerator DoesDebuffHitAll()
    {
        WaveSpawner testWaveSpawner = GameObject.Instantiate<WaveSpawner>(_spawner);
        BuildingTypeSO type = _towers[0];
        GameObject towerObject = GameObject.Instantiate(type.prefab, Vector3.zero, Quaternion.identity);
        Tower tower = towerObject.GetComponent<Tower>();
        type.SetParameters(tower);
        yield return new WaitForEndOfFrame();
        Spawner testSpawner = testWaveSpawner.gameObject.GetComponentInChildren<Spawner>();

        for (int i = 0; i < 5; i++)
        {
            testWaveSpawner.SpawnWave();
            yield return new WaitForSeconds(testWaveSpawner.GetCurrentWave().batches[0].intervalBetweenEnemies * 2f);
            testWaveSpawner.StopSpawning();
        }
        foreach (GameObject go in testSpawner.spawns)
        {
            go.GetComponent<Enemy>().autoNavigate = false;
            go.GetComponent<Enemy>().SetSpeed(0);
            go.transform.position = tower.transform.position;

        }

        yield return new WaitForSeconds(type.attackRate);
        foreach (GameObject go in testSpawner.spawns)
        {
            BuffSO buff = go.GetComponent<Enemy>().appliedBuffs[towerObject.GetComponent<AOETower>().effect].buff;

            Assert.AreEqual(towerObject.GetComponent<AOETower>().effect, buff);
        }
    }

}