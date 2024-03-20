using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetWaveNumber : MonoBehaviour
{
    IWaveSpawnStrategy _waveSpawnStrategy;
    TextMeshProUGUI _textMesh;
    private void Awake()
    {
        _waveSpawnStrategy = GameObject.FindGameObjectWithTag("WaveSpawner").GetComponent<IWaveSpawnStrategy>();
        _textMesh = GetComponent<TextMeshProUGUI>();
        _textMesh.text = "Wave 1/" + _waveSpawnStrategy.GetMaxWaveNr().ToString();
    }

    private void setWaveNumText()
    {
        _textMesh.text = "Wave " + (_waveSpawnStrategy.GetCurrentWaveNr()+1).ToString() + "/" + _waveSpawnStrategy.GetMaxWaveNr().ToString();
    }

    private void OnEnable()
    {
        WaitingState.onWaveEnded += setWaveNumText;
    }
    private void OnDisable()
    {
        WaitingState.onWaveEnded -= setWaveNumText;
    }
}
