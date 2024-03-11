using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateTimer : MonoBehaviour
{
    BuilderState _builderState;
    AudioSource _audioSource;
    private TextMeshProUGUI _countdownText;
    [SerializeField] int _timeAlmostUp = 5;

    private void Awake()
    {
        _builderState = GameObject.FindGameObjectWithTag("LevelFSM").GetComponent<BuilderState>();
        _countdownText = GetComponent<TextMeshProUGUI>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        BuilderState.onClockTick += updateTimer;
    }
    private void OnDisable()
    {
        BuilderState.onClockTick -= updateTimer;
    }
    private void updateTimer(int seconds)
    {
        _countdownText.text = seconds.ToString();
        if(seconds <= _timeAlmostUp) _audioSource.Play();
    }

}
