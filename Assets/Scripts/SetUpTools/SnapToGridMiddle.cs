using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
[ExecuteInEditMode]

public class SnapToGridMiddle : MonoBehaviour
{
    private void Awake()
    {
        transform.position = new Vector3 ((float)Math.Round(transform.position.x / 5.0f) * 5.0f, 0, (float)Math.Round(transform.position.z / 5.0f) * 5.0f);
        
    }
}
