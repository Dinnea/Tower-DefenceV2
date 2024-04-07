using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.AI;

public class PathfinderTest
{
    WaveSpawner _waveSpawner;
    GameObject[] _navPoints;
    float testSpeed = 10000;

    //OneTimeSetup is a NUnit attribute, it identify methods that are called once
    //prior to executing any of the tests.
    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("PathfinderTestScene");
    }
    [UnitySetUp]
    public IEnumerator SetupTests()
    {
        //Wait for the scene to finish loading. 
        yield return new WaitForSeconds(0.5f);

        //Then setup the tests.
        _waveSpawner = GameObject.FindAnyObjectByType<WaveSpawner>();
        _navPoints = GameObject.FindGameObjectsWithTag("NavPoint");

    }

    [UnityTest]
    public IEnumerator FindFirstPoint()
    {
        WaveSpawner testWaveSpawner = GameObject.Instantiate<WaveSpawner>(_waveSpawner);
        testWaveSpawner.SpawnWave();
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        Enemy testEnemy = enemy.GetComponent<Enemy>();
        testEnemy.autoNavigate = false;
        enemy.GetComponent<NavMeshAgent>().speed = testSpeed;
        enemy.GetComponent <NavMeshAgent>().acceleration = testSpeed;
       
        yield return new WaitForSeconds(1f);

        Vector3 targetNav = Vector3.zero;
        foreach (GameObject nav in _navPoints)
        {
            if (nav.GetComponent<NavPoint>().index == 0) targetNav = nav.GetComponent<NavPoint>().GetNavLocation();
        }
        testEnemy.Move(targetNav);

        Vector3 correctYPosition = targetNav;
        correctYPosition.y = enemy.transform.position.y;
        Assert.AreEqual(enemy.transform.position, correctYPosition);
        GameObject.Destroy(enemy);
    }

    [UnityTest]
    public IEnumerator FindPoint2After1()
    {
        WaveSpawner testWaveSpawner = GameObject.Instantiate<WaveSpawner>(_waveSpawner);
        testWaveSpawner.SpawnWave();
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        Enemy testEnemy = enemy.GetComponent<Enemy>();
        testEnemy.autoNavigate = false;
        enemy.GetComponent<NavMeshAgent>().speed = testSpeed;
        enemy.GetComponent<NavMeshAgent>().acceleration = testSpeed;

        Vector3 targetNav = Vector3.zero;
        foreach (GameObject nav in _navPoints)
        {
            if (nav.GetComponent<NavPoint>().index == 0) targetNav = nav.GetComponent<NavPoint>().GetNavLocation();
        }
        testEnemy.Move(targetNav);
        yield return new WaitForSeconds(1f);

        Vector3 correctYPosition = targetNav;
        correctYPosition.y = enemy.transform.position.y;
        Assert.AreEqual(enemy.transform.position, correctYPosition);

        targetNav = Vector3.zero;
        foreach (GameObject nav in _navPoints)
        {
            if (nav.GetComponent<NavPoint>().index == 1) targetNav = nav.GetComponent<NavPoint>().GetNavLocation();
        }
        testEnemy.Move(targetNav);
        yield return new WaitForSeconds(1f);

        correctYPosition = targetNav;
        correctYPosition.y = enemy.transform.position.y;
        Assert.AreEqual(enemy.transform.position, correctYPosition);
        GameObject.Destroy(enemy);
    }

    [UnityTest]
    public IEnumerator FindThreePoints()
    {
        WaveSpawner testWaveSpawner = GameObject.Instantiate<WaveSpawner>(_waveSpawner);
        testWaveSpawner.SpawnWave();
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        Enemy testEnemy = enemy.GetComponent<Enemy>();
        testEnemy.autoNavigate = false;
        enemy.GetComponent<NavMeshAgent>().speed = testSpeed;
        enemy.GetComponent<NavMeshAgent>().acceleration = testSpeed;

        Vector3 targetNav = Vector3.zero;
        foreach (GameObject nav in _navPoints)
        {
            if (nav.GetComponent<NavPoint>().index == 0) targetNav = nav.GetComponent<NavPoint>().GetNavLocation();
        }
        testEnemy.Move(targetNav);
        yield return new WaitForSeconds(1f);

        Vector3 correctYPosition = targetNav;
        correctYPosition.y = enemy.transform.position.y;
        Assert.AreEqual(enemy.transform.position, correctYPosition);

        targetNav = Vector3.zero;
        foreach (GameObject nav in _navPoints)
        {
            if (nav.GetComponent<NavPoint>().index == 1) targetNav = nav.GetComponent<NavPoint>().GetNavLocation();
        }
        testEnemy.Move(targetNav);
        yield return new WaitForSeconds(1f);

        correctYPosition = targetNav;
        correctYPosition.y = enemy.transform.position.y;
        Assert.AreEqual(enemy.transform.position, correctYPosition);

        targetNav = Vector3.zero;
        foreach (GameObject nav in _navPoints)
        {
            if (nav.GetComponent<NavPoint>().index == 2) targetNav = nav.GetComponent<NavPoint>().GetNavLocation();
        }
        testEnemy.Move(targetNav);
        yield return new WaitForSeconds(1f);

        correctYPosition = targetNav;
        correctYPosition.y = enemy.transform.position.y;
        Assert.AreEqual(enemy.transform.position, correctYPosition);

        GameObject.Destroy(enemy);
    }


}