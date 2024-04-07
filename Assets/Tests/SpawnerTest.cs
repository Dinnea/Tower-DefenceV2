using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;

public class SpawnerTest
{
    WaveSpawner _waveSpawner;

    //OneTimeSetup is a NUnit attribute, it identify methods that are called once
    //prior to executing any of the tests.
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("SpawnerTestScene");
    }
    [UnitySetUp]
    public IEnumerator SetupTests()
    {
        //Wait for the scene to finish loading. 
        yield return new WaitForSeconds(0.5f);

        //Then setup the tests.
        //_spawner 
        _waveSpawner = GameObject.FindObjectOfType<WaveSpawner>();
    }

    [UnityTest]
    public IEnumerator EnemyTypeTest()
    {
        WaveSpawner testWaveSpawner = GameObject.Instantiate<WaveSpawner>(_waveSpawner);
        Spawner testSpawner = testWaveSpawner.gameObject.GetComponentInChildren<Spawner>();
        yield return new WaitForEndOfFrame();
        testWaveSpawner.SpawnWave();
        yield return new WaitForSeconds(testWaveSpawner.GetCurrentWave().batches[0].intervalBetweenEnemies);
        testWaveSpawner.StopSpawning();
        GameObject spawned = testSpawner.spawns[0];
        Assert.IsNotNull(spawned.GetComponent<Enemy>());
        Enemy spawnedEnemy = spawned.GetComponent<Enemy>();

        EnemySO enemyType = testWaveSpawner.GetCurrentWave().batches[0].enemyType;

        Component intendedScript = enemyType.AddEnemyScript(new GameObject());
        Assert.AreEqual(intendedScript.GetType(), spawnedEnemy.GetType());
 
    }

    [UnityTest]
    public IEnumerator EnemySpeedTest()
    {
        WaveSpawner testWaveSpawner = GameObject.Instantiate<WaveSpawner>(_waveSpawner);
        Spawner testSpawner = testWaveSpawner.gameObject.GetComponentInChildren<Spawner>();
        yield return new WaitForEndOfFrame();
        testWaveSpawner.SpawnWave();
        yield return new WaitForSeconds(testWaveSpawner.GetCurrentWave().batches[0].intervalBetweenEnemies);
        testWaveSpawner.StopSpawning();
        GameObject spawned = testSpawner.spawns[0];
        Assert.IsNotNull(spawned.GetComponent<Enemy>());
        Enemy spawnedEnemy = spawned.GetComponent<Enemy>();

        EnemySO enemyType = testWaveSpawner.GetCurrentWave().batches[0].enemyType;

        Assert.AreEqual(enemyType.speed, spawnedEnemy.GetSpeed());
    }

    [UnityTest]
    public IEnumerator EnemyHealthTest()
    {
        WaveSpawner testWaveSpawner = GameObject.Instantiate<WaveSpawner>(_waveSpawner);
        Spawner testSpawner = testWaveSpawner.gameObject.GetComponentInChildren<Spawner>();
        yield return new WaitForEndOfFrame();
        testWaveSpawner.SpawnWave();
        yield return new WaitForSeconds(testWaveSpawner.GetCurrentWave().batches[0].intervalBetweenEnemies);
        testWaveSpawner.StopSpawning();
        GameObject spawned = testSpawner.spawns[0];
        Assert.IsNotNull(spawned.GetComponent<Enemy>());
        Enemy spawnedEnemy = spawned.GetComponent<Enemy>();

        EnemySO enemyType = testWaveSpawner.GetCurrentWave().batches[0].enemyType;

        Assert.AreEqual(enemyType.health, spawnedEnemy.GetMaxHealth());
    }
    [UnityTest]
    public IEnumerator EnemyMoneyTest()
    {
        WaveSpawner testWaveSpawner = GameObject.Instantiate<WaveSpawner>(_waveSpawner);
        Spawner testSpawner = testWaveSpawner.gameObject.GetComponentInChildren<Spawner>();
        yield return new WaitForEndOfFrame();
        testWaveSpawner.SpawnWave();
        yield return new WaitForSeconds(testWaveSpawner.GetCurrentWave().batches[0].intervalBetweenEnemies);
        testWaveSpawner.StopSpawning();
        GameObject spawned = testSpawner.spawns[0];
        Assert.IsNotNull(spawned.GetComponent<Enemy>());
        Enemy spawnedEnemy = spawned.GetComponent<Enemy>();

        EnemySO enemyType = testWaveSpawner.GetCurrentWave().batches[0].enemyType;

        Assert.AreEqual(enemyType.money, spawnedEnemy.GetMoney());
    }

    [UnityTest]
    public IEnumerator EnemyDmgTest()
    {
        WaveSpawner testWaveSpawner = GameObject.Instantiate<WaveSpawner>(_waveSpawner);
        Spawner testSpawner = testWaveSpawner.gameObject.GetComponentInChildren<Spawner>();
        yield return new WaitForEndOfFrame();
        testWaveSpawner.SpawnWave();
        yield return new WaitForSeconds(testWaveSpawner.GetCurrentWave().batches[0].intervalBetweenEnemies);
        testWaveSpawner.StopSpawning();
        GameObject spawned = testSpawner.spawns[0];
        Assert.IsNotNull(spawned.GetComponent<Enemy>());
        Enemy spawnedEnemy = spawned.GetComponent<Enemy>();

        EnemySO enemyType = testWaveSpawner.GetCurrentWave().batches[0].enemyType;

        Assert.AreEqual(enemyType.dmg, spawnedEnemy.GetDmg());
    }

    [UnityTest]
    public IEnumerator EnemyVisualTest()
    {
        WaveSpawner testWaveSpawner = GameObject.Instantiate<WaveSpawner>(_waveSpawner);
        Spawner testSpawner = testWaveSpawner.gameObject.GetComponentInChildren<Spawner>();
        yield return new WaitForEndOfFrame();
        testWaveSpawner.SpawnWave();
        yield return new WaitForSeconds(testWaveSpawner.GetCurrentWave().batches[0].intervalBetweenEnemies);
        testWaveSpawner.StopSpawning();
        GameObject spawned = testSpawner.spawns[0];
        Assert.IsNotNull(spawned.GetComponent<Enemy>());
        Enemy spawnedEnemy = spawned.GetComponent<Enemy>();

        EnemySO enemyType = testWaveSpawner.GetCurrentWave().batches[0].enemyType;
        Assert.AreEqual(enemyType.visual.GetComponentInChildren<MeshFilter>().sharedMesh, spawnedEnemy.GetComponentInChildren<MeshFilter>().sharedMesh);
    }
    [UnityTest]
    public IEnumerator EnemyCountTest()
    {
        WaveSpawner testWaveSpawner = GameObject.Instantiate<WaveSpawner>(_waveSpawner);
        Spawner testSpawner = testWaveSpawner.gameObject.GetComponentInChildren<Spawner>();
        yield return new WaitForEndOfFrame();
        testWaveSpawner.SpawnWave();
        yield return new WaitForSeconds(testWaveSpawner.GetCurrentWave().batches[0].intervalBetweenEnemies * testWaveSpawner.GetCurrentWave().batches[0].enemyNumber);
        testWaveSpawner.StopSpawning();
        Assert.AreEqual(testWaveSpawner.GetCurrentWave().batches[0].enemyNumber, testSpawner.spawns.Count);
        
    }

    [UnityTest]
    public IEnumerator TypeChangetest()
    {
        WaveSpawner testWaveSpawner = GameObject.Instantiate<WaveSpawner>(_waveSpawner);
        Spawner testSpawner = testWaveSpawner.gameObject.GetComponentInChildren<Spawner>();
        yield return new WaitForEndOfFrame();
        testWaveSpawner.SpawnWave();
        yield return new WaitForSeconds(testWaveSpawner.GetCurrentWave().batches[0].intervalBetweenEnemies * testWaveSpawner.GetCurrentWave().batches[0].enemyNumber);
        yield return new WaitForSeconds(testWaveSpawner.GetCurrentWave().batches[1].intervalBetweenEnemies);
        testWaveSpawner.StopSpawning();
        Assert.AreNotEqual(testSpawner.spawns[0], testSpawner.spawns.Last());

    }
}