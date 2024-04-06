using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffSO : ScriptableObject
{
    public float duration;
    protected IBuffable _target;
    public virtual void OnApply(IBuffable target)
    {
    }
    public abstract void Execute(IBuffable target);
    public virtual void OnRemove(IBuffable target)
    {
    }

    public IEnumerator Duration()
    {
        yield return new WaitForSeconds(duration);
        OnRemove(_target);
    }
}

