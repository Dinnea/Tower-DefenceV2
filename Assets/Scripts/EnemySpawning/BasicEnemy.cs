using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static EventBus<Event>;

[RequireComponent(typeof(IgnoreCollisionSameLayer))]
public class BasicEnemy : MonoBehaviour, IEnemy, IAttackable, IBuffable
{
    NavMeshAgent _agent;
    [SerializeField] Vector3[] _navPoints;
    int _pointsReached = 0;

    float _maxHealth;
    float _health;
    [SerializeField] float _speed;
    float _dmg;
    [SerializeField] float _money;

    public Action<float> onTakeDamage { get; set; }
    List<BuffSO> IBuffable.appliedBuffs { get; set; }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        GameObject[] navPoints = GameObject.FindGameObjectsWithTag("NavPoint");
        _navPoints = new Vector3[navPoints.Length];
        foreach(GameObject navPoint in navPoints)
        {
            NavPoint point = navPoint.GetComponent<NavPoint>();
            _navPoints[point.index] = point.GetNavLocation();
        }
        
    }
    private void Update()
    {
        //TakeDmg(0.1f);
        Move();
        if (_health < 0)
        {
            Die();
        }
    }
    public void Move()
    {
        if(_pointsReached < _navPoints.Length)
        {
            _agent.destination = _navPoints[_pointsReached];
            if (transform.position.x == _navPoints[_pointsReached].x && transform.position.z == _navPoints[_pointsReached].z)
            {
                _pointsReached++;
            }
        }
    }

    public void Die(bool killed = true)
    {
        if(killed)EventBus<EnemyKilledEvent>.Publish(new EnemyKilledEvent(this));
        Destroy(gameObject);
    }

    public void SetHealth(float pHealth)
    {
        _health = pHealth;
        _maxHealth = pHealth;
    }

    public void SetSpeed(float pSpeed)
    {
        _speed = pSpeed;
        _agent.speed = pSpeed;
    }

    public void SetMoney(float pMoney)
    {
        _money = pMoney;
    }

    public float GetMoney()
    {
        return _money;
    }

    public Vector3 GetWorldLocation()
    {
        return transform.position;
    }

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

    public void SetDmg(float dmg)
    {
        _dmg = dmg;//throw new NotImplementedException();
    }

    public float GetDmg() { return _dmg;}
    public float GetSpeed() { return _speed; }



    public List<BuffSO> GetBuffs()
    {
        throw new NotImplementedException();
    }

    public void ApplyBuff(BuffSO buff)
    {
        throw new NotImplementedException();
    }

    public void RemoveBuff(BuffSO buff)
    {
        throw new NotImplementedException();
    }

    public void RemoveBuff(int index)
    {
        throw new NotImplementedException();
    }
}
