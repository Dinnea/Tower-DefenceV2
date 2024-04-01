using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    CapsuleCollider test;

    private void Awake()
    {
        test.radius = 20;
    }
}
