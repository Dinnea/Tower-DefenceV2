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
public enum TransactionMode { BUILD, SELL, UPGRADE, NONE}
public class Builder : MonoBehaviour
{
    Cursor _cursor;
    [SerializeField] List<BuildingTypeSO> _buildings;
    BuildingTypeSO _buildingType = null;
    MoneyManager _moneyManager;
    TransactionMode _transactionMode = TransactionMode.NONE;

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
        ChangeMode(TransactionMode.BUILD);
        if (buildingID >= 0 && buildingID < _buildings.Count)
        {
            _buildingType = _buildings[buildingID];
        }
        else _buildingType = null;
    }
    /// <summary>
    /// Chooses a building to build based on provided BuiuldingTypeSo. Null if not present in the _buildings list. Disables selling and upgrading.
    /// </summary>
    /// <param name="buildingID"></param>
    public void SetBuildingType(BuildingTypeSO type)
    {
        ChangeMode(TransactionMode.BUILD);
        Debug.Log("yea");
       if(_buildings.Contains(type)) _buildingType = type;
       else _buildingType = null;

    }
    /// <summary>
    /// Chooses a building to build based on BuildingTypeSO given by BuildingSwitchedEvent. Null if not present in the _buildings list. Disables selling and upgrading.
    /// </summary>
    /// <param name="buildingSwitchedEvent"></param>
    public void SetBuildingType(BuildingSwitchedEvent buildingSwitchedEvent)
    {
        SetBuildingType(buildingSwitchedEvent.buildingType);
    }

    public void ChangeMode(TransactionMode mode)
    {
        _transactionMode = mode;
        if (mode != TransactionMode.BUILD) _buildingType = null;
        _cursor.SetTransactionMode(mode);
    }

    public void SwitchSell()
    {
        if(_transactionMode == TransactionMode.SELL) ChangeMode(TransactionMode.NONE);
        else ChangeMode(TransactionMode.SELL);
    }
    public void SwitchUpgrade()
    {
        if(_transactionMode == TransactionMode.UPGRADE) ChangeMode(TransactionMode.NONE);
        else ChangeMode(TransactionMode.UPGRADE);
    }
    private void processClick(ClickInfo info)
    {

        switch (_transactionMode)
        {
            case TransactionMode.BUILD:
                if(_buildingType != null)buildTower(info.clickedCell, info.clickedCellWorldLoc);
                break;
            case TransactionMode.SELL:
                sellTower(info.clickedCell);
                break;
            case TransactionMode.UPGRADE:
                upgradeTower(info.clickedCell, info.clickedCellWorldLoc);
                break;

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
            if (!DebugSystem.IsInfiniteMoney()) CheckWhatCanAfford(_moneyManager.CalculateTransaction(_buildingType.cost));
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
       if(!DebugSystem.IsInfiniteMoney())CheckWhatCanAfford(_moneyManager.CalculateTransaction(toBuild.cost));
        
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
            if (!DebugSystem.IsInfiniteMoney()) CheckWhatCanAfford(_moneyManager.CalculateTransaction(cell.GetValueOnCell(), true));
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
            if (newUpgrade != null && _moneyManager.GetMoney() >= newUpgrade.cost)
            {
                GameObject toDestroy = cell.GetObjectOnTile().gameObject;
                cell.ResetObjectOnCell(false);
                Destroy(toDestroy);
                buildTower(cell, location, newUpgrade);
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
    public void CheckWhatCanAfford(float money)
    {
       if(_buildingType != null) if(_buildingType.cost > money) resetBuildChoice();
    }
    public List<BuildingTypeSO> GetBuildOptions()
    {
        return _buildings;
    }
    private void Update()
    {
        //UpgradeSwitch();
    }
}
