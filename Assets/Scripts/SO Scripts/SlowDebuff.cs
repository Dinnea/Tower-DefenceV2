using UnityEngine;

[CreateAssetMenu(fileName = "slow debuff", menuName = "Debuffs/Slow")]
public class SlowDebuff : BuffSO
{
    [Range(0, 1)] public float speedModifier = 0.5f;
    float _originalSpeed;
    public override void OnApply(IBuffable target)
    {
        target.ApplyBuff(this);
        //_originalSpeed = target.GetSpeed();
        //target.SetSpeed(_originalSpeed * speedModifier);
    }

    public override void OnRemove(IBuffable target)
    {
        target.RemoveBuff(this);
        //target.SetSpeed(_originalSpeed);
    }
}