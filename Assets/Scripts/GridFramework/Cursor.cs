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

    MeshFilter _cursorModel;
    
    List<Mesh> _cursorOptions = new List<Mesh>();
    Mesh _defaultCursor;
    Vector3 _defaultCursorScale;

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
        _cursorModel = GetComponentInChildren<MeshFilter>();
        _defaultCursor = _cursorModel.mesh;
        _defaultCursorScale = _cursorModel.transform.localScale;
    }
    void Update()
    {       
        cursorSnapToGrid();
    }
    public void AddCursorOption(Mesh mesh)
    {
        _cursorOptions.Add(mesh);
    }

    public void SetCursorModel(int buildingID)
    {
        if (buildingID >= 0 && buildingID < _cursorOptions.Count)
        {
            _cursorModel.mesh = _cursorOptions[buildingID];
            _cursorModel.transform.localScale = new Vector3(2, 2, 2);
        }
        else
        {
            _cursorModel.mesh = _defaultCursor;
            _cursorModel.transform.localScale = _defaultCursorScale;
        }
    }
    private void cursorSnapToGrid()
    {
        _cursorLocation = VectorMath.GetMousePositionWorld(Camera.main, _layer);
        Vector2Int targetCell = _grid.GetCellOnWorldPosition(_cursorLocation);
        //Debug.Log(targetCell);
        if (_grid.CheckInBounds(targetCell.x, targetCell.y))//if isnt out of bounds, change location, get ready to send click info to observers
        {
            transform.position = new Vector3(_grid.GetCellPositionInWorld(targetCell.x, targetCell.y).x, _cursorHeight, _grid.GetCellPositionInWorld(targetCell.x, targetCell.y).z);
            clickOnCell(targetCell);
        }
    }
    private void clickOnCell(Vector2Int targetCell)
    {
        if (Input.GetMouseButtonDown(0))
        {
            onClick?.Invoke(new ClickInfo(targetCell, _grid.GetCellPositionInWorld(targetCell.x, targetCell.y), _grid.GetCellContent(targetCell.x, targetCell.y)));
        }
    }
}
