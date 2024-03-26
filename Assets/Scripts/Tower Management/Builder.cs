using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Personal.Utilities;
using UnityEngine.UIElements;
using System;
using Unity.VisualScripting;
using static EventBus<Event>;

public class Builder : MonoBehaviour
{
    Cursor _cursor;
    [SerializeField] List<BuildingTypeSO> _buildings;
    BuildingTypeSO _buildingType = null;
    bool _sell = false;
    MoneyManager _moneyManager;

    private void Awake()
    {
        _cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Cursor>();
        _moneyManager = GetComponent<MoneyManager>();
    }
    private void OnEnable()
    {
       _cursor.onClick += processClick;
        EventBus<BuildingSwitchedEvent>.OnEvent += SetBuildingType;
    }
    private void OnDisable()
    {
        _cursor.onClick -= processClick;
        EventBus<BuildingSwitchedEvent>.OnEvent -= SetBuildingType;
    }
    public void SetBuildingType(int buildingID)
    {
        if(buildingID >= 0 && buildingID < _buildings.Count)
        {
            _buildingType = _buildings[buildingID];
        }
        else _buildingType = null;
    }

    public void SetBuildingType(BuildingTypeSO type)
    {
       if(_buildings.Contains(type)) _buildingType = type;
       else _buildingType = null;
    }

    public void SetBuildingType(BuildingSwitchedEvent buildingSwitchedEvent)
    {
        Debug.Log("attempt1");
        _buildingType = buildingSwitchedEvent.buildingType;
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
            cell.AddValueToCell(_buildingType.cost);
            checkIfCanAffordCurrentBuilding(_moneyManager.CalculateTransaction(_buildingType.cost));
        }
    }

    private void sellTower(Cell cell)
    {
        if (!cell.IsCellFree())
        {
            Transform buildingToSell = cell.GetObjectOnTile();
            BuildingTypeSO buildingToSellType = cell.GetObjectOnTileType();
            cell.ResetObjectOnTile();
            checkIfCanAffordCurrentBuilding(_moneyManager.CalculateTransaction(cell.GetValueOnCell(), true));
            Destroy(buildingToSell.gameObject);
        }
    }
    private void resetBuildChoice()
    {
        _buildingType = null;
        _cursor.SetCursorDefault();
    }
    private void checkIfCanAffordCurrentBuilding(float money)
    {
       if(_buildingType != null) if(_buildingType.cost > money) resetBuildChoice();
    }
    public List<BuildingTypeSO> GetBuildOptions()
    {
        return _buildings;
    }
}
