using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffSO : ScriptableObject
{
    public float duration;
    protected float timer;
    public virtual void OnApply(IBuffable target)
    {
        timer = duration;
    }
    public abstract void OnRemove(IBuffable target);

    protected virtual void Countdown()
    {

    }
}

