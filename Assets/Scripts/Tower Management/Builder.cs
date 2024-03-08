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

    public Action<TransactionData> onBuild;
    public Action<TransactionData> onSale;

    private void Awake()
    {
        _cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Cursor>();
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
        buildTower(info.clickedCell, info.clickedCellWorldLoc);
        resetBuildChoice();
    }

    private void buildTower(Cell cell, Vector3 location)
    {
        if (cell.CanBuild() && _buildingType != null)
        {
            Transform built = Instantiate(_buildingType.prefab.transform, location, Quaternion.identity);
            cell.SetObjectOnTile(built);

            onBuild?.Invoke(new TransactionData(_buildingType.cost));
        }
    }
    private void resetBuildChoice()
    {
        _buildingType = null;
        _cursor.SetCursorModel(-1);
    }
    private void sendTowerModelsToCursor()
    {
        foreach (BuildingTypeSO building in _buildings)
        {
            Transform towerModel = Search.FindComponentInChildrenWithTag<Transform>(building.prefab, "TowerMesh");
            _cursor.AddCursorOption(towerModel.GetComponent<MeshFilter>().sharedMesh);
        }
    }
    public List<BuildingTypeSO> GetBuildOptions()
    {
        return _buildings;
    }
}

public class TransactionData
{
    public float cost;
    public TransactionData(float pCost)
    {
        cost = pCost;
    }
}