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
    bool _sell = false;

    public Action<TransactionData> onBuild;
    public Action<TransactionData> onSale;

    [SerializeField][Range(0, 1)] float _resaleValue = 0.75f;

    private void Awake()
    {
        _cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Cursor>();
        sendTowerModelsToCursor();
    }
    private void OnEnable()
    {
       _cursor.onClick += processClick;
    }
    private void OnDisable()
    {
        _cursor.onClick -= processClick;
    }
    public void SetBuildingType(int buildingID)
    {
        if(buildingID >= 0 && buildingID < _buildings.Count)
        {
            _buildingType = _buildings[buildingID];
        }
        else _buildingType = null;
    }
    public void DisableSell()
    {
        _sell = false;
    }
    public void SellSwitch()
    {
        _sell = !_sell;
    }
    private void processClick(Cursor.ClickInfo info)
    {
        if (_buildingType != null)
        {
            buildTower(info.clickedCell, info.clickedCellWorldLoc);
            resetBuildChoice();
        }
        if (_sell)
        {
            sellTower(info.clickedCell);
        }
    }

    private void buildTower(Cell cell, Vector3 location)
    {
        if (cell.CanBuild())
        {
            Transform built = Instantiate(_buildingType.prefab.transform, location, Quaternion.identity);
            cell.SetObjectOnTile(built, _buildingType);

            onBuild?.Invoke(new TransactionData(-_buildingType.cost));
        }
    }
    private void sellTower(Cell cell)
    {
        if (!cell.IsCellFree())
        {
            Transform buildingToSell = cell.GetObjectOnTile();
            BuildingTypeSO buildingToSellType = cell.GetObjectOnTileType();
            cell.ResetObjectOnTile();
            float income = buildingToSellType.cost * _resaleValue;
            onSale?.Invoke(new TransactionData(income));
            Destroy(buildingToSell.gameObject);
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
    public float moneyChange;
    public TransactionData(float pMoneyChange)
    {
        moneyChange = pMoneyChange;
    }
}