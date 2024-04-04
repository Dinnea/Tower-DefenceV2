using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
[ExecuteInEditMode]


public class SnapToGridMiddle : MonoBehaviour
{
    [SerializeField] bool _moveByHalf = false;
    private void Awake()
    {
        SnapToMiddle(_moveByHalf);        
    }
    /// <summary>
    /// Moves the object to the middle of a grid cell. Some objects need to be moved by an additional half cell.
    /// </summary>
    /// <param name="moveByHalf"></param>
    public void SnapToMiddle(bool moveByHalf)
    {
        if(moveByHalf) transform.position = new Vector3((float)Math.Round(transform.position.x / 5.0f) * 5.0f, 0, (float)Math.Round(transform.position.z / 5.0f) * 5.0f) + new Vector3(2.5f, 0, 2.5f);
        else transform.position = new Vector3((float)Math.Round(transform.position.x / 5.0f) * 5.0f, 0, (float)Math.Round(transform.position.z / 5.0f) * 5.0f);
    }
}
