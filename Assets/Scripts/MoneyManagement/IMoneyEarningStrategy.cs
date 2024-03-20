using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoneyEarningStrategy
{
    public static Action<FLoatingMoneyData> onMoneyRecieved;
    public void GainMoney();
    public void SpawnFloatingText();
}

public class FLoatingMoneyData
{
    public int money;
    public Vector3 location;
    public FLoatingMoneyData(int pMoney, Vector3 pLocation)
    {
        money = pMoney;
       location = pLocation;
    }
}
