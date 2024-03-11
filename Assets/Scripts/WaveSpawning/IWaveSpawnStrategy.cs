using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaveSpawnStrategy 
{
    public void SpawnWave();
    public void OnWaveFinished();

    public static Action onFinishedSpawning;
}
