using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpLevel : MonoBehaviour
{
    List<Vector3> _pathZones;
    GridManager _gridManager;
    GridXZ<Cell> _grid;
    List<Cell> _path = new List<Cell>();
    public GameObject test;
    private void Start()
    {
        _gridManager = GetComponent<GridManager>();
        _grid =_gridManager.GetGrid();
        getPathTiles();
    }
    void getPathTiles()
    {
        GameObject[] pathZones = GameObject.FindGameObjectsWithTag("DeleteOnAwakePath");
        foreach (GameObject path in pathZones)
        {
            //Vector2Int gridCoords = _grid.GetCellOnWorldPosition();
            Cell cell = _grid.GetCellContent(path.transform.position);
            cell.SetPath();
            cell.SetBuildZone(false);
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


            //set all neighbouring tiles to buildZone
            //unless they are path.
            //O O O
            //O X O
            //O O O

        }

        for (int x = 0; x< _grid.GetWidthInColumns(); x++)
        {
            for (int z = 0; z < _grid.GetHeightInRows(); z++)
            {
                if (_grid.GetCellContent(x, z).isBuildZone())
                Instantiate(test, _grid.GetCellPositionInWorld(x, z), Quaternion.identity);
            }
        }
    }

    void trySetCellAsBuildZone(int x, int z)
    {
        if( (x > 0 && x < _grid.GetWidthInColumns()) && (z > 0 && z < _grid.GetHeightInRows()) ) 
        {
            Cell targetCell = _grid.GetCellContent(x, z);
            if (targetCell.IsPath() == false) targetCell.SetBuildZone();
        }
        
    }
}
