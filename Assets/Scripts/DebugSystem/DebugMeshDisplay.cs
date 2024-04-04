using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMeshDisplay : MonoBehaviour
{
    private void Awake()
    {
        enableMesh(DebugSystem.debug);
    }
    /// <summary>
    /// Turns the visual of the object on and off.
    /// </summary>
    /// <param name="value"></param>
    private void enableMesh(bool value)
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.enabled = value;
        }
    }

    private void OnEnable()
    {
        DebugSystem.onDebugStateChanged += enableMesh;
    }
    private void OnDisable()
    {
        DebugSystem.onDebugStateChanged -= enableMesh;
    }
}
