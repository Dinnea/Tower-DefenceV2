using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectGameOver : MonoBehaviour
{
    [SerializeField] GameObject _winText;
    [SerializeField] GameObject _lossText;
    [SerializeField] GameObject _restartText;
    RawImage _image;
    private void Awake()
    {
        _image = GetComponent<RawImage>();
    }

    private void OnEnable()
    {
        HQ.onDie += enableLoss;
        WaveSpawner.onWavesOver += enableWin;
    }
    private void OnDisable()
    {
        HQ.onDie -= enableLoss;
        WaveSpawner.onWavesOver -= enableWin;
    }

    private void enableWin()
    {
        _winText.SetActive(true);
        gameOver();
    }

    private void enableLoss()
    {
       _lossText.SetActive(true);
        gameOver();
    }

    private void gameOver()
    {
        _restartText.SetActive(true);
        _image.enabled = true;
    }
}
