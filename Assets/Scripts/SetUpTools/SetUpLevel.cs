using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Personal.GridFramework;

public class SetUpLevel : MonoBehaviour
{
    GridManager _gridManager;
    GridXZ<Cell> _grid;
    public GameObject valid;
    public GameObject invalid;
    public GameObject path;
    [SerializeField] BuildingTypeSO _HQ;
    private void Start()
    {
        _gridManager = GetComponent<GridManager>();
        _grid =_gridManager.GetGrid();
        setHQ();
        getPathTiles();
    }

    /// <summary>
    /// Find the location of the HQ. Set occupied Cells accordingly.
    /// </summary>
    void setHQ()
    {
        GameObject hq = GameObject.FindGameObjectWithTag("HQ");
        Vector2Int hqCell = _grid.GetCellOnWorldPosition(hq.transform.position);
        List<Vector2Int> hqCellsCoords = _HQ.GetCellsCovered(hqCell);
        foreach (Vector2Int coords in hqCellsCoords)
        {
            Cell cell = _grid.GetCellContent(coords.x, coords.y);
            cell.SetHQ(true);
        }
    }
    /// <summary>
    /// Find all cells marked as a path for the enemies. Marks surrounding tiles as buildzones IF they are not path/HQ zones. 
    /// Can override buildzone as path if appropriate.
    /// </summary>
    void getPathTiles()
    { 

        GameObject[] pathZones = GameObject.FindGameObjectsWithTag("DeleteOnAwakePath");
        foreach (GameObject path in pathZones)
        {
            //Vector2Int gridCoords = _grid.GetCellOnWorldPosition();
            Cell cell = _grid.GetCellContent(path.transform.position);
            if (_grid.CheckInBounds(cell.GetX(), cell.GetZ()))
            {
                cell.SetPath();
                cell.SetBuildZone(false);
                //set all neighbouring tiles to buildZone
                //unless they are path
                //left
                trySetCellAsBuildZone(cell.GetX() - 1, cell.GetZ() - 1);
                trySetCellAsBuildZone(cell.GetX() - 1, cell.GetZ());
                trySetCellAsBuildZone(cell.GetX() - 1, cell.GetZ() + 1);
                //mid
                trySetCellAsBuildZone(cell.GetX(), cell.GetZ() + 1);
                trySetCellAsBuildZone(cell.GetX(), cell.GetZ() - 1);
                //right
                trySetCellAsBuildZone(cell.GetX() + 1, cell.GetZ() - 1);
                trySetCellAsBuildZone(cell.GetX() + 1, cell.GetZ());
                trySetCellAsBuildZone(cell.GetX() + 1, cell.GetZ() + 1);
            }           
        }
        
        for (int x = 0; x < _grid.GetWidthInColumns(); x++)
        {
            for (int z = 0; z < _grid.GetHeightInRows(); z++)
            {
                if (_grid.GetCellContent(x, z).IsBuildZone())
                {
                    Instantiate(valid, _grid.GetCellPositionInWorld(x, z), Quaternion.identity);
                }
                else if (_grid.GetCellContent(x, z).IsHQ())
                {
                    Instantiate(path, _grid.GetCellPositionInWorld(x, z), Quaternion.identity);
                }
                else if (!_grid.GetCellContent(x, z).IsPath())
                {
                    Instantiate(invalid, _grid.GetCellPositionInWorld(x, z), Quaternion.identity);
                }
            }
        }
    }

    /// <summary>
    /// Markes a given cell as buildzone, IF it isn't yet a path/HQ zone.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    void trySetCellAsBuildZone(int x, int z)
    {
        if(_grid.CheckInBounds(x, z)) 
        { 
            Cell targetCell = _grid.GetCellContent(x, z);
            if (!targetCell.IsPath() && !targetCell.IsHQ()) targetCell.SetBuildZone();
        }
        
    }
}
