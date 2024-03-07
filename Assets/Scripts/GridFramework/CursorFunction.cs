using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Personal.Utilities;

public class CursorFunction : MonoBehaviour
{
    [SerializeField] LayerMask _layer;
    [SerializeField] GridManager _gridManager;
    GridXZ<Cell> _grid;
    Vector3 _cursorLocation = Vector3.zero;

    private void Start()
    {
        _grid = _gridManager.GetGrid();
    }
    void Update()
    {
        
        _cursorLocation = Utilities.GetMousePositionWorld(Camera.main, _layer);
        Vector2Int targetCell = _grid.GetCellOnWorldPosition(_cursorLocation);
        //Debug.Log(targetCell);
        if (targetCell.x!= -1 && targetCell.y != -1)//if isnt out of bounds, change location
        { 
            transform.position = _grid.GetCellPositionInWorld(targetCell.x, targetCell.y);
        }

        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log(_grid.GetCellContent(targetCell.x, targetCell.y).IsBuildZone());
            Debug.Log(targetCell);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(_grid.GetCellContent(targetCell.x, targetCell.y).IsPath());
            Debug.Log(targetCell);
        }
    }
}
