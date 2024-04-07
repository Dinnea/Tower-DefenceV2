using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSystem : MonoBehaviour
{
    public static bool debug = true;
    public static Action<bool> onDebugStateChanged;
    private static bool _infiniteMoney = false;
    private static bool _instaKill = false;
    private static bool _isImmortal = false;

    public static bool IsInfiniteMoney()
    {
        return _infiniteMoney;
    }

    private void toggleInfiniteMoney()
    {
        _infiniteMoney = !_infiniteMoney;
        if (_infiniteMoney)
        {
            FindObjectOfType<Builder>().CheckWhatCanAfford(1000000000000000);
            FindObjectOfType<MoneyDisplay>().SetInfinite();
        }
        else
        {

            FindObjectOfType<Builder>().CheckWhatCanAfford(FindObjectOfType<MoneyManager>().CalculateTransaction(0));
        }
    }
    public static bool IsInstakill()
    {
        return _instaKill;
    }
    private void toggleInstakill()
    {
        _instaKill =!_instaKill;
    }

    public static bool IsImmortal()
    {
        return _isImmortal;
    }

    private void toggleImmortal()
    {
        _isImmortal= !_isImmortal;
    }

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

    private void allDie()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = temp.Length - 1; i >= 0; i--)
        {
            temp[i].GetComponent<Enemy>()?.Die();
        }
    }

    public void RecieveCommand(string command)
    {
        if(command == "powerwordkill")
        {
            toggleInstakill();
        }
        else if (command == "motherlode")
        {
           toggleInfiniteMoney();
           
        }
        else if(command == "immortal")
        {
            toggleImmortal();
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
