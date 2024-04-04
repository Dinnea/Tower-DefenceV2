using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography;
using System;
using Unity.VisualScripting;
using static EventBus<Event>;

public class BuildSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject _towerPreview;
    TextMeshProUGUI[] _statsText;
    TextMeshProUGUI _buttonText;
    Button _button;
    BuildingTypeSO _buildingType;
    MoneyManager _moneyManager;
    //public BuildingSwitchedEvent onSwitched = new(null);

    private void Awake()
    {
        _moneyManager = GameObject.FindGameObjectWithTag("Player").GetComponent<MoneyManager>();
        _towerPreview = FindObjectOfType<TowerPreview>(true).gameObject;
        _statsText = _towerPreview.GetComponentsInChildren<TextMeshProUGUI>(true);
        _buttonText = GetComponentInChildren<TextMeshProUGUI>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(onClickTrigger);        
    }
    /// <summary>
    /// Enables tower preview on hover.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {       
        _towerPreview.SetActive(true);
        applyData();
    }
    /// <summary>
    /// Disables tower preview on end hover.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        _towerPreview.SetActive(false);
    }

    private void OnEnable()
    {
        _moneyManager.onMoneyChanged += checkAfford;
    }
    private void OnDisable()
    {
       _moneyManager.onMoneyChanged -= checkAfford;
    }

    /// <summary>
    /// Applies data of the _buildingTye coressponding to the hovered over button.
    /// </summary>
    private void applyData()
    {
        _statsText[0].text = "Level 1";
        _statsText[1].text = _buildingType.attackType;
        _statsText[2].text = "Dmg per hit: " + _buildingType.damage.ToString();
        _statsText[3].text = "Range: " + _buildingType.range.ToString();
        _statsText[4].text = "Attack interval " + _buildingType.attackRate.ToString();
        _statsText[5].text = "Cost: " + _buildingType.cost.ToString();
    }
    private void checkAfford(float money)
    {
        if(money < _buildingType.cost)
        {
            _button.interactable = false;
        }
        else _button.interactable = true;
    }

    /// <summary>
    /// Assing building type to the button.
    /// </summary>
    /// <param name="buildingType"></param>
    public void AssignBuildingType(BuildingTypeSO buildingType)
    {
        _buttonText.text = buildingType.nameString;
        _buildingType = buildingType;
    }

    public BuildingTypeSO GetAssignedType()
    {
        return _buildingType;
    }

    /// <summary>
    /// Triggers when the button is clicked, signals that new building has been selected.
    /// </summary>
    private void onClickTrigger()
    {
        EventBus<BuildingSwitchedEvent>.Publish(new BuildingSwitchedEvent(_buildingType));
    }
}
