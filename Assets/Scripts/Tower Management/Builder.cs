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

    [SerializeField] float _money = 1000;
    [SerializeField][Range(0, 1)] float _resaleValue = 0.75f;
    public Action<MoneyChangedData> onMoneyChanged;

    private void Awake()
    {
        _cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Cursor>();
        sendTowerModelsToCursor();
    }

    private void Start()
    {
        refreshMoneyBalance();
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
            //resetBuildChoice();
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
            _money -= _buildingType.cost;
            refreshMoneyBalance();
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
            _money += income;
            refreshMoneyBalance();
            Destroy(buildingToSell.gameObject);
        }
    }
    private void resetBuildChoice()
    {
        _buildingType = null;
        _cursor.SetCursorModel(-1);
    }
    private void refreshMoneyBalance()
    {
       
        for (int i = 0; i < _buildings.Count; i++)
        {
            bool canAffordCurrent = canAfford(_buildings[i].cost);
            if (_buildingType == _buildings[i] && !canAffordCurrent) resetBuildChoice();
            onMoneyChanged?.Invoke(new MoneyChangedData(i, _money, canAffordCurrent));
        }
    }
    private bool canAfford(float cost)
    {
        return (_money - cost) >= 0;
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
public class MoneyChangedData
{
    public readonly int buildingID;
    public readonly float currentMoney;
    public readonly bool canAfford;
    public MoneyChangedData(int pBuildingID, float pCurrentMoney, bool pCanAfford)
    {
        buildingID = pBuildingID;
        currentMoney = pCurrentMoney;
        canAfford = pCanAfford;
    }
}