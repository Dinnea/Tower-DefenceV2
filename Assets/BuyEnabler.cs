using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyEnabler : MonoBehaviour
{
    MoneyManager _moneyManager;
    Button[] _buttons;

    private void Awake()
    {
        _moneyManager = GameObject.FindGameObjectWithTag("Player").GetComponent<MoneyManager>();
        _buttons = GetComponentsInChildren<Button>();
    }

    private void OnEnable()
    {
        _moneyManager.onMoneyChanged += enableButton;
    }

    private void OnDisable()
    {
        _moneyManager.onMoneyChanged -= enableButton;
    }

    private void enableButton(MoneyChangedData data)
    {
        _buttons[data.buildingID].interactable = data.canAfford;
    }
}
