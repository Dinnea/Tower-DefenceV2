using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public abstract class Enemy : MonoBehaviour, IAttackable, IBuffable 
{
    protected float _maxHealth;
    protected float _health;
    protected float _speed;
    protected float _dmg;
    protected float _money;

    protected NavMeshAgent _agent;
    public bool autoNavigate = true;
    public Action<float> onTakeDamage { get; set; }

    //[SerializeField] List<BuffSO> _appliedBuffs = new List<BuffSO>();
    [SerializeField]Dictionary<BuffSO, AppliedBuff> _appliedBuffs = new Dictionary<BuffSO, AppliedBuff>();

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
        if (_health <= 0)
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

    public void TryAddBuff(BuffSO buff)
    {
        if (!_appliedBuffs.ContainsKey(buff))
        {
            _appliedBuffs.Add(buff, new AppliedBuff(RunBuffDuration(buff), Instantiate(buff.FX, transform)));
            buff.OnApply(this);
            StartCoroutine(_appliedBuffs[buff].duration);
        }
        else
        {
            RefreshBuffDuration(buff);
        }
    }

    public void RemoveBuff(BuffSO buff)
    {
        Destroy(_appliedBuffs[buff].instantiatedFX);
        _appliedBuffs.Remove(buff);
        buff.OnRemove(this);
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public IEnumerator RunBuffDuration(BuffSO buff)
    {
        yield return new WaitForSeconds(buff.duration);
        RemoveBuff(buff);
    }

    public void RefreshBuffDuration(BuffSO buff)
    {
        StopCoroutine(_appliedBuffs[buff].duration);
        StartCoroutine(_appliedBuffs[buff].duration);
    }

    public class AppliedBuff
    {
        public IEnumerator duration;
        public GameObject instantiatedFX;
        public AppliedBuff(IEnumerator duration,GameObject instantiatedFX)
        {
            this.duration = duration;
            this.instantiatedFX = instantiatedFX;
        }
    }
}
