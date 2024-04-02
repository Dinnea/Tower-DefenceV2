using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuffable
{
    public List<BuffSO> appliedBuffs {  get; set; }
    public void ApplyBuff(BuffSO buff);
    public void RemoveBuff(BuffSO buff);
    public void RemoveBuff(int index);
    public List<BuffSO> GetBuffs();

}
