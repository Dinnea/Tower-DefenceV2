using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaveSpawnStrategy 
{
    public void SpawnWave();
    public void OnWaveFinished();
    public int GetCurrentWaveNr();
    public int GetMaxWaveNr();

    public static Action onFinishedSpawning;
}
