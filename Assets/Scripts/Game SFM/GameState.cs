using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    [SerializeField] protected LevelFSM _levelSFM;
    [SerializeField] protected string _stateID;
    
    public void Init(LevelFSM pLevelSFM)
    {
        _levelSFM = pLevelSFM;
    }
    /// <summary>
    /// Override this to do things at the start of state.
    /// </summary>
    public abstract void OnEnterState();
    /// <summary>
    /// Override this to do things on update while the state is active.
    /// </summary>
    public abstract void Handle();
    /// <summary>
    /// Override this to do things at the end of the state.
    /// </summary>
    public abstract void OnExitState();
}
