using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    Builder _builder;
    private TextMeshProUGUI _moneyDisplay;
    private void Awake()
    {
        _builder = GameObject.FindGameObjectWithTag("Player").GetComponent<Builder>();
        _moneyDisplay = GetComponent<TextMeshProUGUI>();
        //setMoney(1000);
    }

    private void OnEnable()
    {
       MoneyManager.onMoneyChanged += setMoney;
    }

    private void OnDisable()
    {
        MoneyManager.onMoneyChanged -= setMoney;
    }

    private void setMoney(float money)
    {
        _moneyDisplay.text = "Money: " + money.ToString();
    }
}
