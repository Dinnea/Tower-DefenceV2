using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static EventBus<Event>;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] float _money = 1000;
    [SerializeField] MoneyEarningStrategy _moneyStrategy;
    [SerializeField] TransactionStrategy _buyStrategy;
    [SerializeField] TransactionStrategy _sellStrategy;
    public static Action<float> onMoneyChanged;

    private void Start()
    {
        onMoneyChanged?.Invoke(_money);
    }
    private void OnEnable()
    {
        EventBus<EnemyKilledEvent>.OnEvent += onEnemyKilled;
    }
    private void OnDisable()
    {
        EventBus<EnemyKilledEvent>.OnEvent -= onEnemyKilled;
    }

    private void onEnemyKilled(EnemyKilledEvent enemyKilledEvent)
    {
        _money += _moneyStrategy.GetMoneyFromEnemy(enemyKilledEvent.enemy);
        onMoneyChanged?.Invoke(_money);
    }

   public float CalculateTransaction(float value, bool isSell = false)
    {
        if (!isSell) _money -= _buyStrategy.CalculateTransaction(value);
        else _money += _sellStrategy.CalculateTransaction(value);
        onMoneyChanged?.Invoke(_money);
        return _money;
    }

    public float GetMoney() { return _money; }
}
