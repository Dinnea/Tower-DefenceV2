using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetUpLevel : MonoBehaviour
{
    List<Vector3> _pathZones;
    GridManager _gridManager;
    GridXZ<Cell> _grid;
    List<Cell> _path = new List<Cell>();
    public GameObject valid;
    public GameObject invalid;
    public bool debug = true;
    [SerializeField] BuildingTypeSO _HQ;
    private void Start()
    {
        _gridManager = GetComponent<GridManager>();
        _grid =_gridManager.GetGrid();
        setHQ();
        getPathTiles();
    }

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
        
        if (debug) 
        {
            for (int x = 0; x < _grid.GetWidthInColumns(); x++)
            {
                for (int z = 0; z < _grid.GetHeightInRows(); z++)
                {
                    if (_grid.GetCellContent(x, z).IsBuildZone())
                    {
                        Instantiate(valid, _grid.GetCellPositionInWorld(x, z), Quaternion.identity);
                    }
                    else Instantiate(Instantiate(invalid, _grid.GetCellPositionInWorld(x, z), Quaternion.identity));

                }
            }
        }
        //for (int i = pathZones.Length; i--> 0;)
        //{
        //    Destroy(pathZones[i]);
        //}
    }

    void trySetCellAsBuildZone(int x, int z)
    {
        if(_grid.CheckInBounds(x, z)) 
        { 
            Cell targetCell = _grid.GetCellContent(x, z);
            if (!targetCell.IsPath() && !targetCell.IsHQ()) targetCell.SetBuildZone();
        }
        
    }
}
