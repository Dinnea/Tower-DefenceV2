using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void speedUpTime2()
    {
        Time.timeScale = 2.0f;
    }
    void speedUpTime3()
    {
        Time.timeScale = 3.0f;
    }

    void normalTime()
    {
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) Restart();
        if (Input.GetKeyDown(KeyCode.Q)) speedUpTime2();
        if (Input.GetKeyDown(KeyCode.W)) speedUpTime3();
        if(Input.GetKeyDown(KeyCode.E)) normalTime();
    }
}
