using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Personal.Utilities;
using UnityEngine.Events;
using System;

public class Cursor : MonoBehaviour
{
    [SerializeField] LayerMask _layer;
    [SerializeField] float _cursorHeight = 0.5f;
    GridXZ<Cell> _grid;
    Vector3 _cursorLocation = Vector3.zero;

    MeshFilter _cursorModel;
    Renderer _cursorRenderer;
    
    List<Mesh> _cursorOptions = new List<Mesh>();
    Mesh _defaultCursor;
    Vector3 _defaultCursorScale;

    [SerializeField] Material _validMaterial;
    [SerializeField] Material _invalidMaterial;

    public Action<ClickInfo> onClick;

    public class ClickInfo
    {
        //public Vector2Int clickedCellCoords;
        public Vector3 clickedCellWorldLoc;
        public Cell clickedCell;
        public ClickInfo(Vector3 pClickedCellWorldLoc, Cell pClickedCell)
        {
            //clickedCellCoords = pClickedCellCoords;
            clickedCellWorldLoc = pClickedCellWorldLoc;
            clickedCell = pClickedCell;
        }
    }

    private void Start()
    {
        _grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>().GetGrid(); ;
        _cursorModel = GetComponentInChildren<MeshFilter>();
        _cursorRenderer = GetComponentInChildren<Renderer>();
        _defaultCursor = _cursorModel.mesh;
        _defaultCursorScale = _cursorModel.transform.localScale;
    }
    void Update()
    {       
        cursorMoveOnGrid();
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
    private void cursorMoveOnGrid()
    {
        _cursorLocation = VectorMath.GetMousePositionWorld(Camera.main, _layer);
        Vector2Int cellCoords = _grid.GetCellOnWorldPosition(_cursorLocation);
        //Debug.Log(targetCell);
        if (_grid.CheckInBounds(cellCoords.x, cellCoords.y))//if isnt out of bounds, change location, get ready to send click info to observers
        {
            transform.position = new Vector3(_grid.GetCellPositionInWorld(cellCoords.x, cellCoords.y).x, _cursorHeight, _grid.GetCellPositionInWorld(cellCoords.x, cellCoords.y).z);
            Cell targetCell = _grid.GetCellContent(cellCoords.x, cellCoords.y);
            //apply visual
            if (targetCell.CanBuild())
            {
                _cursorRenderer.material = _validMaterial;        
            }
            else
            {
                _cursorRenderer.material = _invalidMaterial;
            }
            clickOnCell(targetCell);
        }
    }
    private void clickOnCell( Cell targetCell)
    {
        if (Input.GetMouseButtonDown(0))
        {
            onClick?.Invoke(new ClickInfo(_grid.GetCellPositionInWorld(targetCell.GetX(), targetCell.GetZ()), targetCell));
        }
    }
}
