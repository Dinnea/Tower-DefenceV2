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
    public Action<BuildingTypeSO> onClickEvent;
    BuildingTypeSO _buildingType;
    //public BuildingSwitchedEvent onSwitched = new(null);

    private void Awake()
    {
        _towerPreview = FindObjectOfType<TowerPreview>(true).gameObject;
        _statsText = _towerPreview.GetComponentsInChildren<TextMeshProUGUI>(true);
        _buttonText = GetComponentInChildren<TextMeshProUGUI>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(onClickTrigger);        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {       
        _towerPreview.SetActive(true);
        applyData();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _towerPreview.SetActive(false);
    }

    private void OnEnable()
    {
        MoneyManager.onMoneyChanged += checkAfford;
    }
    private void OnDisable()
    {
        MoneyManager.onMoneyChanged -= checkAfford;
    }

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
    public void AssignBuildingType(BuildingTypeSO buildingType)
    {
        _buttonText.text = buildingType.nameString;
        _buildingType = buildingType;
    }

    public BuildingTypeSO GetAssignedType()
    {
        return _buildingType;
    }

    private void onClickTrigger()
    {
        EventBus<BuildingSwitchedEvent>.Publish(new BuildingSwitchedEvent(_buildingType));
    }
}
