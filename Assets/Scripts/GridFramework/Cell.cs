using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void ResetObjectOnTile()
    {
        _objectOnTile = null;
        _objectType = null;
    }

    public Transform GetObjectOnTile()
    {
        return _objectOnTile;
    }

    public BuildingTypeSO GetObjectOnTileType()
    {
        return _objectType;
    }

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
