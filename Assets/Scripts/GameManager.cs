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

    public static void ScaleTime(KeyCode key, float scale)
    {
        if (Input.GetKeyDown(key))
        {
            ScaleTime(scale);
        }
    }
    public static void ScaleTime(float scale)
    {
        Time.timeScale = scale;
        Debug.Log(Time.timeScale);
    }

    /// <summary>
    /// Control Shift D to enableDebug
    /// </summary>
    void enableDebug()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.D))
        {
            DebugSystem.EnableDebug();
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.R)) Restart();
            ScaleTime(KeyCode.F1, 0);
            ScaleTime(KeyCode.F2, 1);
            ScaleTime(KeyCode.F3, 2);
            ScaleTime(KeyCode.F4, 3);
            
            enableDebug();
            
        }
       
    }
}
