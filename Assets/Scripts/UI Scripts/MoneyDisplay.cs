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
    }

    private void OnEnable()
    {
       _builder.onMoneyChanged += setMoney;
    }

    private void OnDisable()
    {
        _builder.onMoneyChanged -= setMoney;
    }

    private void setMoney(MoneyChangedData data)
    {
        _moneyDisplay.text = "Money: " + data.currentMoney.ToString();
    }

}
