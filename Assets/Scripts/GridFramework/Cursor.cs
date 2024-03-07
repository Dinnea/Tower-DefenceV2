using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Personal.Utilities;
using UnityEngine.Events;
using System;

public class Cursor : MonoBehaviour
{
    [SerializeField] LayerMask _layer;
    [SerializeField] GridManager _gridManager;
    [SerializeField] float _cursorHeight = 0.5f;
    GridXZ<Cell> _grid;
    Vector3 _cursorLocation = Vector3.zero;
    public Action<ClickInfo> onClick;

    public class ClickInfo
    {
        public Vector2Int clickedCellCoords;
        public Vector3 clickedCellWorldLoc;
        public Cell clickedCell;
        public ClickInfo(Vector2Int pClickedCellCoords, Vector3 pClickedCellWorldLoc, Cell pClickedCell)
        {
            clickedCellCoords = pClickedCellCoords;
            clickedCellWorldLoc = pClickedCellWorldLoc;
            clickedCell = pClickedCell;
        }
    }

    private void Start()
    {
        _grid = _gridManager.GetGrid();
    }
    void Update()
    {
        
        _cursorLocation = Utilities.GetMousePositionWorld(Camera.main, _layer);
        Vector2Int targetCell = _grid.GetCellOnWorldPosition(_cursorLocation);
        //Debug.Log(targetCell);
        if (_grid.CheckInBounds(targetCell.x, targetCell.y))//if isnt out of bounds, change location, get ready to send click info to observers
        { 
            transform.position = new Vector3(_grid.GetCellPositionInWorld(targetCell.x, targetCell.y).x, _cursorHeight, _grid.GetCellPositionInWorld(targetCell.x, targetCell.y).z);
            if (Input.GetMouseButtonDown(0))
            {
                onClick?.Invoke(new ClickInfo(targetCell, _grid.GetCellPositionInWorld(targetCell.x, targetCell.y), _grid.GetCellContent(targetCell.x, targetCell.y)));
            }
        }

        
        
    }
}
