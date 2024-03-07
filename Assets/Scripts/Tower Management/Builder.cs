using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Personal.Utilities;
using UnityEngine.UIElements;
using System;
using Unity.VisualScripting;

public class Builder : MonoBehaviour
{
    Cursor _cursor;
    [SerializeField] List<BuildingTypeSO> _buildings;
    BuildingTypeSO _buildingType = null;

    private void Awake()
    {
        _cursor = FindAnyObjectByType<Cursor>();
    }

    private void OnEnable()
    {
       _cursor.onClick += clicked;
    }
    private void OnDisable()
    {
        _cursor.onClick -= clicked;
    }
    public void SetBuildingType(int buildingID)
    {
        if(buildingID >= 0 && buildingID < _buildings.Count)
        {
            _buildingType = _buildings[buildingID];
        }
        else _buildingType = null;
    }
    private void clicked(Cursor.ClickInfo info)
    {    
        if (info.clickedCell.CanBuild() && _buildingType!= null)
        {            
            Transform built = Instantiate(_buildingType.prefab.transform, info.clickedCellWorldLoc, Quaternion.identity);
            info.clickedCell.SetObjectOnTile(built);
        }
        
    }
}
