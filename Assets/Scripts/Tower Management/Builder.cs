using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Personal.Utilities;
using UnityEngine.UIElements;
using System;
using Unity.VisualScripting;
using static EventBus<Event>;
using System.Threading;
public class Builder : MonoBehaviour
{
    Cursor _cursor;
    [SerializeField] List<BuildingTypeSO> _buildings;
    BuildingTypeSO _buildingType = null;
    bool _sell = false;
    MoneyManager _moneyManager;
    bool _upgrade = false;

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
        DisableSell();
        DisableUpgrade();
    }

    public void SetBuildingType(BuildingTypeSO type)
    {
        Debug.Log("yea");
       if(_buildings.Contains(type)) _buildingType = type;
       else _buildingType = null;
        DisableSell();
        DisableUpgrade();

    }

    public void SetBuildingType(BuildingSwitchedEvent buildingSwitchedEvent)
    {
        _buildingType = buildingSwitchedEvent.buildingType;
        DisableSell();
        DisableUpgrade();
    }
    public void DisableSell()
    {
        _sell = false;
    }
    public void SellSwitch()
    {
        _sell = !_sell;
    }
    public void DisableUpgrade()
    {
        _upgrade = false;
    }
    public void UpgradeSwitch()
    {
        _upgrade = !_upgrade;
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
        if (_upgrade)
        {
            upgradeTower(info.clickedCell, info.clickedCellWorldLoc);
        }
    }

    private void buildTower(Cell cell, Vector3 location)
    {
        if (cell.CanBuild())
        {
            Transform built = Instantiate(_buildingType.prefab.transform, location, Quaternion.identity);
            cell.SetObjectOnTile(built, _buildingType);
            cell.AddValueToCell(_buildingType.cost);
            _buildingType.SetParameters(built.GetComponent<Tower>());
            checkIfCanAffordCurrentBuilding(_moneyManager.CalculateTransaction(_buildingType.cost));
        }
    }

    private void buildTower(Cell cell, Vector3 location, BuildingTypeSO upgrade)
    {
       Transform built = Instantiate(upgrade.prefab.transform, location, Quaternion.identity);
       cell.SetObjectOnTile(built, upgrade);
       cell.AddValueToCell(upgrade.cost);
       upgrade.SetParameters(built.GetComponent<Tower>());
       checkIfCanAffordCurrentBuilding(_moneyManager.CalculateTransaction(upgrade.cost));
        
    }

    private void sellTower(Cell cell)
    {
        if (!cell.IsCellFree())
        {
            Transform buildingToSell = cell.GetObjectOnTile();
            checkIfCanAffordCurrentBuilding(_moneyManager.CalculateTransaction(cell.GetValueOnCell(), true));
            cell.ResetObjectOnCell(true);
            Destroy(buildingToSell.gameObject);
        }
    }

    private void upgradeTower(Cell cell, Vector3 location)
    {
        if(!cell.IsCellFree())
        {
            
            BuildingTypeSO newUpgrade = cell.GetObjectOnTileType().upgrade;
            Debug.Log(newUpgrade);
            if(newUpgrade != null)
            {
                // Debug.Log("attempt");
                GameObject toDestroy = cell.GetObjectOnTile().gameObject;
                cell.ResetObjectOnCell(false);
                Destroy(toDestroy);
                buildTower(cell, location, newUpgrade);
            }
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
