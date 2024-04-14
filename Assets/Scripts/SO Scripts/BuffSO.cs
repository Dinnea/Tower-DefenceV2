using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffSO : ScriptableObject
{
    public float duration;
    protected IBuffable _target;
    public GameObject FX;
    public abstract void OnApply(IBuffable target);
    public abstract void OnRemove(IBuffable target);
}

