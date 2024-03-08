using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    MoneyManager _moneyManager;
    private TextMeshProUGUI _moneyDisplay;
    private void Awake()
    {
        _moneyManager = GameObject.FindGameObjectWithTag("Player").GetComponent<MoneyManager>();
        _moneyDisplay = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
       _moneyManager.onMoneyChanged += setMoney;
    }

    private void OnDisable()
    {
        _moneyManager.onMoneyChanged -= setMoney;
    }

    private void setMoney(MoneyChangedData data)
    {
        _moneyDisplay.text = "Money: " + data.currentMoney.ToString();
    }


}
