using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffSO : ScriptableObject
{
    public abstract void OnApply(IEnemy target);
    public abstract void OnRemove(IEnemy target);
}

public class SlowDebuff : BuffSO
{
    [Range(0, 1)]public float speedModifier = 0.5f;
    float _originalSpeed;
    public override void OnApply(IEnemy target)
    {
        _originalSpeed = target.GetSpeed();
        target.SetSpeed(_originalSpeed*speedModifier);
    }

    public override void OnRemove(IEnemy target)
    {
        target.SetSpeed(_originalSpeed);
    }
}
