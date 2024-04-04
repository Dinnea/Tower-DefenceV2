using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    private TextMeshProUGUI _moneyDisplay;
    private MoneyManager _moneyManager;
    private void Awake()
    {
        _moneyDisplay = GetComponent<TextMeshProUGUI>();
        _moneyManager = GameObject.FindGameObjectWithTag("Player").GetComponent<MoneyManager>();
    }

    private void OnEnable()
    {
       _moneyManager.onMoneyChanged += setMoney;
    }

    private void OnDisable()
    {
        _moneyManager.onMoneyChanged -= setMoney;
    }

    private void setMoney(float money)
    {
        _moneyDisplay.text = "Money: " + money.ToString();
    }
}
