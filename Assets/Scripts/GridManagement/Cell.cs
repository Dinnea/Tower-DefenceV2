using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Personal.GridFramework;

public class Cell 
{
    GridXZ<Cell> _grid;
    int _x, _z;
    bool _isBuildZone = false;
    bool _isPath = false;
    Transform _objectOnTile = null;
    BuildingTypeSO _objectType = null;
    public string name = "no";
    bool _isHq = false;
    float _valueOnCell;

    public Cell(GridXZ<Cell> grid, int x, int z)
    {
        _grid = grid;
        _x = x;
        _z = z;
    }
    public void SetName(string pname)
    {
        name = pname;
    }
    public void SetObjectOnTile(Transform objectToPlace, BuildingTypeSO objectType)
    {
        _objectOnTile = objectToPlace;
        _objectType = objectType;
        _grid.TriggerGridObjectChanged(_x, _z);
    }
    /// <summary>
    /// ResetValue determines should the money on the cell be cleared?
    /// </summary>
    /// <param name="resetValue"></param>
    public void ResetObjectOnCell(bool resetValue)
    {
        _objectOnTile = null;
        _objectType = null;
        if(resetValue)ResetValueOnCell();
    }
    /// <summary>
    /// Stores money on cell
    /// </summary>
    /// <param name="value"></param>
    public void AddValueToCell(float value)
    {
        _valueOnCell += value;
    }
    /// <summary>
    /// Clears the money from cell.
    /// </summary>
    public void ResetValueOnCell()
    {
        _valueOnCell = 0;
    }
    /// <summary>
    /// Returns amount of money on cell.
    /// </summary>
    /// <returns></returns>
    public float GetValueOnCell()
    {
        return _valueOnCell;
    }
    /// <summary>
    /// Get the actual cloned object from tile.
    /// </summary>
    /// <returns></returns>
    public Transform GetObjectOnTile()
    {
        return _objectOnTile;
    }
    /// <summary>
    /// Get the BuildingTypeSO that contains information about the object on tile.
    /// </summary>
    /// <returns></returns>
    public BuildingTypeSO GetObjectOnTileType()
    {
        return _objectType;
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns>Is this a cell build zone and is it taken up by a tower already?</returns>
    public bool CanBuild()
    {
        return _isBuildZone && IsCellFree();
    }
    public bool IsCellFree()
    {
        return _objectOnTile == null;
    }
    public void SetBuildZone(bool value = true) { _isBuildZone=value;}
    public void SetPath(bool value = true) { _isPath=value;}
    public void SetHQ(bool value = true) { _isHq = value; }
    public bool IsPath() { return _isPath;}
    public bool IsHQ() { return _isHq;}
    public bool IsBuildZone() {  return _isBuildZone;}
    public int GetX() { return _x; } public int GetZ() { return _z; }
}
