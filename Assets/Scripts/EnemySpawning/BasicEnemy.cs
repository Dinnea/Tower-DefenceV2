using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static EventBus<Event>;

[RequireComponent(typeof(IgnoreCollisionSameLayer))]
public class BasicEnemy : Enemy, IAttackable, IBuffable
{
    [SerializeField] Vector3[] _navPoints;
    int _pointsReached = 0;
    public Action<float> onTakeDamage { get; set; }
    List<BuffSO> _appliedBuffs = new List<BuffSO>();

// -------------- Abstract Enemy ----------------//
    protected override void OnAwake()
    {
        base.OnAwake();
        GameObject[] navPoints = GameObject.FindGameObjectsWithTag("NavPoint");
        _navPoints = new Vector3[navPoints.Length];
        foreach (GameObject navPoint in navPoints)
        {
            NavPoint point = navPoint.GetComponent<NavPoint>();
            _navPoints[point.index] = point.GetNavLocation();
        }
    }
    protected override void OnUpdate()
    {
        move();
        base.OnUpdate();
    }

    private void move()
    {
        if (_pointsReached < _navPoints.Length)
        {
            if(Move(_navPoints[_pointsReached])) _pointsReached++;
        }
    }
 //----------------- IAttackable -------------- //
    public float GetMaxHealth()
    {
        return _maxHealth;
    }

    public float GetHealth()
    {
        return _health;
    }

    public void TakeDmg(float damage)
    {
        _health -= damage;
        onTakeDamage?.Invoke(_health);
    }

//-------------- IBuffable ----------------//
    public List<BuffSO> GetBuffs()
    {
        return _appliedBuffs;
    }

    public void ApplyBuff(BuffSO buff)
    {
        if(!_appliedBuffs.Contains(buff)) _appliedBuffs.Add(buff);
    }

    public void RemoveBuff(BuffSO buff)
    {
        _appliedBuffs.Remove(buff);
    }

    public void RemoveBuff(int index)
    {
        _appliedBuffs.RemoveAt(index);
    }
}
