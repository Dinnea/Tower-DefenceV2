using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    [SerializeField] protected LevelFSM levelSFM;
    [SerializeField] protected string _stateID;
    public string stateID => _stateID;
    
    public void Init(LevelFSM pLevelSFM)
    {
        levelSFM = pLevelSFM;
    }
    public abstract void OnEnterState();
    public abstract void Handle();
    public abstract void OnExitState();
}
