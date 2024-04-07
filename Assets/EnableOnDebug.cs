using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableOnDebug : MonoBehaviour
{
    [SerializeField] GameObject _debugConsole;
    private void OnEnable()
    {
        DebugSystem.onDebugStateChanged += onDebugChanged;
    }
    private void OnDisable()
    {
        DebugSystem.onDebugStateChanged -= onDebugChanged;
    }

    private void onDebugChanged(bool value)
    {
        _debugConsole.SetActive(value);
    }
}
