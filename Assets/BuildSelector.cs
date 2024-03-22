using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography;
using System;
using Unity.VisualScripting;

public class BuildSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    BuildingTypeSO _buildingType;
    GameObject _towerPreview;
    TextMeshProUGUI[] _statsText;
    TextMeshProUGUI _buttonText;
    Button _button;
    public Action<BuildingTypeSO> onClickEvent;
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
        if(_buildingType != null)
        {
            _towerPreview.SetActive(true);
            applyData();
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _towerPreview.SetActive(false);
    }


    private void applyData()
    {
        _statsText[0].text = "Level 1";
        _statsText[1].text = _buildingType.attackType;
        _statsText[2].text = "Dmg per hit: " + _buildingType.damage.ToString();
        _statsText[3].text = "Range: " + _buildingType.range.ToString();
        _statsText[4].text = "Attack interval " + _buildingType.attackSpeed.ToString();
        _statsText[5].text = "Cost: " + _buildingType.cost.ToString();

    }

    public void AssignBuildingType(BuildingTypeSO buildingType)
    {
        //Debug.Log(buildingType.nameString);
        _buildingType = buildingType;
        _buttonText.text = _buildingType.nameString;
    }

    private void onClickTrigger()
    {
        onClickEvent?.Invoke(_buildingType);
    }
}
