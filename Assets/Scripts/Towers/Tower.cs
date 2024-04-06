using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventBus<Event>;
using static UnityEngine.GraphicsBuffer;

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

    /// <summary>
    /// This function is executed on update. Override it with functionality in derived classes. Do not change Monobehaviour's void Awake().
    /// </summary>
    protected virtual void OnAwake()
    {
        _range = GetComponent<CapsuleCollider>();
    }
    /// <summary>
    /// This function is executed on update. Override it with functionality in derived classes. Do not change Monobehaviour's void Update().
    /// </summary>
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
    /// <summary>
    /// Looks for enemies in range, adds them to the list of enemies in range.
    /// </summary>
    /// <param name="other"></param>
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
    /// <summary>
    /// Removes enemies from the in range list.
    /// </summary>
    /// <param name="other"></param>
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
    /// <summary>
    /// cooldown of the tower's action.
    /// </summary>
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
    protected void applyDamage(GameObject enemy)
    {
        IAttackable target = enemy.GetComponent<IAttackable>();
        _damageStrategy.CalculateDmg(dmg, target);
    }
   
    /// <summary>
    /// Execute tower's given effect. Override in derived classes.
    /// </summary>
    protected virtual void Execute()
    {
        onAction?.Invoke();
        _isOnCooldown = true;
    }
    /// <summary>
    /// Empties the enemies in range to remove missing references.
    /// </summary>
    /// <param name="pEvent"></param>
    protected void ResetTarget(EnemyKilledEvent pEvent)
    {
        enemiesInRange.Clear();
    }
}
