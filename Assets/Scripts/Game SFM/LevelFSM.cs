using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFSM : MonoBehaviour
{
    [SerializeField] GameState _currentState;
    [SerializeField] int countdownBetweenWaves = 15;

    private void OnEnable()
    {
        GameState[] states = GetComponents<GameState>();
        foreach (GameState state in states)
        {
            state.Init(this);
        }
        ChangeState(GetComponent<BuilderState>());

        HQ.onDie += enterGameOverState;
        //WaveSpawner.onWavesOver += enterGameOverState;
    }
    private void OnDisable()
    {
        HQ.onDie -= enterGameOverState;
        //WaveSpawner.onWavesOver -= enterGameOverState;
    }


    private void enterGameOverState()
    {
        ChangeState(GetComponent<GameOverState>());
    }

    private void Update()
    {
        _currentState.Handle();
    }

    public void ChangeState(GameState newState)
    {
        if (_currentState != null)
        {
            _currentState.OnExitState();
        }
        _currentState = newState;
        _currentState.OnEnterState();
    }

    public GameState GetCurrentState()
    {
        return _currentState;
    }

    public int GetCountdownBetweenWaves()
    {
        return countdownBetweenWaves;
    }
}
