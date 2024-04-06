using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuffable
{
    Transform GetTransform();
    public float GetSpeed();
    public void SetSpeed(float speed);

    public void TryAddBuff(BuffSO buff);
    public void RemoveBuff(BuffSO buff);

    public IEnumerator RunBuffDuration(BuffSO buff);
    public void RefreshBuffDuration(BuffSO buff);

}
