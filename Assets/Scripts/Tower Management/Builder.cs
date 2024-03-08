using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        sendTowerModelsToCursor();
    }
    private void OnEnable()
    {
       _cursor.onClick += cursorClicked;
    }
    private void OnDisable()
    {
        _cursor.onClick -= cursorClicked;
    }
    public void SetBuildingType(int buildingID)
    {
        if(buildingID >= 0 && buildingID < _buildings.Count)
        {
            _buildingType = _buildings[buildingID];
        }
        else _buildingType = null;
    }
    private void cursorClicked(Cursor.ClickInfo info)
    {    
        if (info.clickedCell.CanBuild() && _buildingType!= null)
        {            
            Transform built = Instantiate(_buildingType.prefab.transform, info.clickedCellWorldLoc, Quaternion.identity);
            info.clickedCell.SetObjectOnTile(built);
        }
        
    }
    private void sendTowerModelsToCursor()
    {
        foreach (BuildingTypeSO building in _buildings)
        {
            Transform towerModel = Search.FindComponentInChildrenWithTag<Transform>(building.prefab, "TowerMesh");
            _cursor.AddCursorOption(towerModel.GetComponent<MeshFilter>().sharedMesh);
        }
    }
}
