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
    /// <summary>
    /// Chooses a building to build based on provided ID. Null if out of scope. Disables selling and upgrading.
    /// </summary>
    /// <param name="buildingID"></param>
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
    /// <summary>
    /// Chooses a building to build based on provided BuiuldingTypeSo. Null if not present in the _buildings list. Disables selling and upgrading.
    /// </summary>
    /// <param name="buildingID"></param>
    public void SetBuildingType(BuildingTypeSO type)
    {
        Debug.Log("yea");
       if(_buildings.Contains(type)) _buildingType = type;
       else _buildingType = null;
        DisableSell();
        DisableUpgrade();

    }
    /// <summary>
    /// Chooses a building to build based on BuildingTypeSO given by BuildingSwitchedEvent. Null if not present in the _buildings list. Disables selling and upgrading.
    /// </summary>
    /// <param name="buildingSwitchedEvent"></param>
    public void SetBuildingType(BuildingSwitchedEvent buildingSwitchedEvent)
    {
        SetBuildingType(buildingSwitchedEvent.buildingType);
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

    /// <summary>
    /// Build tower on provided cell. Only possible if cell.CanBuild() is true.
    /// </summary>
    /// <param name="cell"></param>
    /// <param name="location"></param>
    private void buildTower(Cell cell, Vector3 location)
    {
        if (cell.CanBuild())
        {
            Transform built = Instantiate(_buildingType.prefab.transform, location, Quaternion.identity);
            cell.SetObjectOnTile(built, _buildingType);
            cell.AddValueToCell(_buildingType.cost);
            _buildingType.SetParameters(built.GetComponent<Tower>());
            checkWhatCanAfford(_moneyManager.CalculateTransaction(_buildingType.cost));
        }
    }
    /// <summary>
    /// Build a chosen building. Ignores cell.CanBuild().
    /// </summary>
    /// <param name="cell"></param>
    /// <param name="location"></param>
    /// <param name="toBuild"></param>
    private void buildTower(Cell cell, Vector3 location, BuildingTypeSO toBuild)
    {
       Transform built = Instantiate(toBuild.prefab.transform, location, Quaternion.identity);
       cell.SetObjectOnTile(built, toBuild);
       cell.AddValueToCell(toBuild.cost);
       toBuild.SetParameters(built.GetComponent<Tower>());
       checkWhatCanAfford(_moneyManager.CalculateTransaction(toBuild.cost));
        
    }
    /// <summary>
    /// Destroys the tower, resets the content of cell.
    /// </summary>
    /// <param name="cell"></param>
    private void sellTower(Cell cell)
    {
        if (!cell.IsCellFree())
        {
            Transform buildingToSell = cell.GetObjectOnTile();
            checkWhatCanAfford(_moneyManager.CalculateTransaction(cell.GetValueOnCell(), true));
            cell.ResetObjectOnCell(true);
            Destroy(buildingToSell.gameObject);
        }
    }

    /// <summary>
    /// If BuildingTypeSO at the location has an upgrade prefab assigned, the tower will be replaced with the prefab. 
    /// The money on tile will not be reset, but added onto
    /// </summary>
    /// <param name="cell"></param>
    /// <param name="location"></param>
    private void upgradeTower(Cell cell, Vector3 location)
    {
        if(!cell.IsCellFree())
        {
            
            BuildingTypeSO newUpgrade = cell.GetObjectOnTileType().upgrade;
            Debug.Log(newUpgrade);
            if (newUpgrade != null && _moneyManager.GetMoney() > newUpgrade.cost)
            {
                GameObject toDestroy = cell.GetObjectOnTile().gameObject;
                cell.ResetObjectOnCell(false);
                Destroy(toDestroy);
                buildTower(cell, location, newUpgrade);
            }
            else
            {
                Debug.Log("you are broke!");
            }
        }
    }
    /// <summary>
    /// Set to no building chosen.
    /// </summary>
    private void resetBuildChoice()
    {
        _buildingType = null;
        _cursor.SetCursorDefault();
    }
    private void checkWhatCanAfford(float money)
    {
       if(_buildingType != null) if(_buildingType.cost > money) resetBuildChoice();
    }
    public List<BuildingTypeSO> GetBuildOptions()
    {
        return _buildings;
    }
}
