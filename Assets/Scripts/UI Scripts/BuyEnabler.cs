using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyEnabler : MonoBehaviour
{
    Builder _builder;
    Button[] _buttons;

    private void Awake()
    {
        _builder = GameObject.FindGameObjectWithTag("Player").GetComponent<Builder>();
        _buttons = GetComponentsInChildren<Button>();
    }

    private void OnEnable()
    {
        _builder.onMoneyChanged += enableButton;
    }

    private void OnDisable()
    {
        _builder.onMoneyChanged -= enableButton;
    }

    private void enableButton(MoneyChangedData data)
    {
        _buttons[data.buildingID].interactable = data.canAfford;
    }
}
