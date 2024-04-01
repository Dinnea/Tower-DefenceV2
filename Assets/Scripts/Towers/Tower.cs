using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventBus<Event>;
public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected List<GameObject> enemiesInRange = new List<GameObject>();
    CapsuleCollider _range;
    protected float actionCD;
    protected float cdLeft;
    protected float dmg;

    public Action onAction;
    protected bool _isOnCooldown = false;

    [SerializeField] protected DamageCalculationStrategy _damageStrategy;

    private void Awake()
    {
        OnAwake();
    }
    private void Update()
    {
        OnUpdate();
    }
    protected virtual void OnAwake()
    {
        _range = GetComponent<CapsuleCollider>();
    }
    protected virtual void OnUpdate()
    {
        cooldown();
    }
    public void SetRange(float range)
    {
        _range.radius = range;
    }
    public float GetRange()
    {
        return _range.radius;
    }
    public void SetActionCD(float cd)
    {
        actionCD = cd;
    }
    public float GetActionCD()
    {
        return actionCD;
    }
    public void SetDMG(float dmg) 
    {
        this.dmg = dmg;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!enemiesInRange.Contains(other.gameObject))
            {
                enemiesInRange.Add(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }

    private void OnEnable()
    {
        EventBus<EnemyKilledEvent>.OnEvent += ResetTarget;
    }

    private void OnDisable()
    {
        EventBus<EnemyKilledEvent>.OnEvent -= ResetTarget;
    }
    private void cooldown()
    {
        if (_isOnCooldown)
        {
            cdLeft -= Time.deltaTime;
            if (cdLeft < 0)
            {
                _isOnCooldown = false;
                cdLeft = actionCD;
            }
        }
    }
    protected void ResetTarget(EnemyKilledEvent pEvent)
    {
        enemiesInRange.Clear();
    }
}
