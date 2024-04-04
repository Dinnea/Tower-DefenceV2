using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour 
{
    protected float _maxHealth;
    protected float _health;
    protected float _speed;
    protected float _dmg;
    protected float _money;

    protected NavMeshAgent _agent;
    //get world loc
    private void Update()
    {
        OnUpdate();
    }
    private void Awake()
    {
        OnAwake();
    }
    /// <summary>
    /// This function is executed on update. Override it with functionality in derived classes. Do not change Monobehaviour's void Update().
    /// </summary>
    protected virtual void OnUpdate()
    {
        if (_health < 0)
        {
            Die();
        }
    }
    /// <summary>
    /// This function is executed on awake. Override it with functionality in derived classes. Do not change Monobehavour's void Awake().
    /// </summary>
    protected virtual void OnAwake()
    {
        _agent = GetComponent<NavMeshAgent>();
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
    /// <summary>
    /// 
    /// </summary>
    /// <returns>How much money does this enemy carry?</returns>
    public float GetMoney() { return _money;}
    public float GetSpeed() { return _speed;}
    public void SetDmg(float dmg)
    {
        _dmg = dmg;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns>How much damage will this enemy deal?</returns>
    public float GetDmg() { return _dmg; }

    /// <summary>
    /// Moves the navmesh agent to the destination.
    /// </summary>
    /// <returns>True when reached destination. </returns>
    public virtual bool Move(Vector3 destination)
    {
        _agent.destination = destination;
        return (transform.position.x == destination.x && transform.position.z == destination.z);
    }
    /// <summary>
    /// Triggers Enemy Killed Event only if killed is true.
    /// </summary>
    /// <param name="killed"> </param>
    public void Die(bool killed = true)
    {
        if (killed) EventBus<EnemyKilledEvent>.Publish(new EnemyKilledEvent(this));
        Destroy(gameObject);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns> Where is the enemy now?s</returns>
    public Vector3 GetWorldLocation() { return transform.position; }
    

}
