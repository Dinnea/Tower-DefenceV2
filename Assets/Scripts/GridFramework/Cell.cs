using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell 
{
    GridXZ<Cell> _grid;
    int _x, _z;
    bool _isBuildZone = false;
    bool _isPath = false;
    Transform _objectOnTile;
    public string name = "no";

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
    public void SetObjectOnTile(Transform objectToPlace)
    {
            _objectOnTile = objectToPlace;
            _grid.TriggerGridObjectChanged(_x, _z);
    }

    public Transform GetObjectOnTile()
    {
        return _objectOnTile;
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
    public bool IsPath() { return _isPath;}
    public bool IsBuildZone() {  return _isBuildZone;}
    public int GetX() { return _x; } public int GetZ() { return _z; }
}
