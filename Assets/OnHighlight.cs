using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class OnHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int _id = 0;
    int _optionsNumber; 
    BuildingTypeSO _buildingType;
    [SerializeField]GameObject _towerPreview;
    TextMeshProUGUI[] _statsText;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_id < _optionsNumber  && _id >= 0)
        {
            _towerPreview.SetActive(true);
            applyData();
        }
        //_mouseover = true;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _towerPreview.SetActive(false);
    }

    private void Awake()
    {
        List<BuildingTypeSO> temp = GameObject.FindGameObjectWithTag("Player").GetComponent<Builder>().GetBuildOptions();
        
        _optionsNumber = temp.Count;
        if (_id < _optionsNumber && _id>=0) _buildingType = temp[_id];
        _statsText = _towerPreview.GetComponentsInChildren<TextMeshProUGUI>(true);
        //_towerPreview.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
