using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSystem : MonoBehaviour
{
    public static bool debug = true;
    public static Action<bool> onDebugStateChanged;

    /// <summary>
    /// ensures debug is disabled on launch
    /// </summary>
    private void Awake()
    {
        debug = true;
        EnableDebug();
    }
    /// <summary>
    /// Enable debug state.
    /// </summary>
    /// <param name="value"></param>
    public static void EnableDebug()
    {
        debug = !debug;
        onDebugStateChanged?.Invoke(debug);
    }

    public void RecieveCommand(string command)
    {
        if(command == "killall")
        {
            GameObject[] temp = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = temp.Length - 1; i >= 0; i--)
            {
                temp[i].GetComponent<Enemy>()?.Die();
            }
        }
        else if (command == "motherlode")
        {
            Debug.Log("infinite money");
        }
        else if(command == "nodie")
        {
            Debug.Log("invincible base");
        }
        else if (command.StartsWith("scale ")) 
        { 
            command = command.Remove(0, 6);
            if (command.All(char.IsDigit))
            {
                GameManager.ScaleTime((float)Convert.ToDouble(command));
            }
            else 
            {
                Debug.Log("Command invalid");
            }
            
        }
        else
        {
            Debug.Log("Command invalid");
        }
    }
}
