using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSystem : MonoBehaviour
{
    public static bool debug = true;
    public static Action<bool> onDebugStateChanged;

    /// <summary>
    /// Enable debug state.
    /// </summary>
    /// <param name="value"></param>
    public static void EnableDebug(bool value)
    {
        debug = value;
        onDebugStateChanged?.Invoke(debug);
    }
}
