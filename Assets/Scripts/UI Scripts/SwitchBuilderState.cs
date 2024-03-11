using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchBuilderState : MonoBehaviour
{
    LevelFSM _fsm;

    private void Awake()
    {
        _fsm = GameObject.FindGameObjectWithTag("LevelFSM").GetComponent<LevelFSM>();
    }
    public void SwitchToOrFromBuilderState()
    {
        if (_fsm.GetCurrentState() is BuilderState) _fsm.ChangeState(_fsm.GetComponent<SpawningState>());
        else _fsm.ChangeState(_fsm.GetComponent<BuilderState>());
    }
}
