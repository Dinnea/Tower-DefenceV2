using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Event { }
public class EventBus<T> where T : Event
{ 
    public static event Action<T> OnEvent;

    public static void Publish(T pEvent)
    {
        OnEvent?.Invoke(pEvent);
    }    
}
/// <summary>
/// Triggered when player selects a new building.
/// </summary>
public class BuildingSwitchedEvent : Event
{
    public BuildingTypeSO buildingType;
    public BuildingSwitchedEvent(BuildingTypeSO pBuildingType)
    {
        buildingType = pBuildingType;
    }
}
/// <summary>
/// Triggered when an enemy is killed.
/// </summary>
public class EnemyKilledEvent : Event
{
    public Enemy enemy;
    public EnemyKilledEvent(Enemy pEnemy) 
    { 
    enemy = pEnemy;
    }
}

