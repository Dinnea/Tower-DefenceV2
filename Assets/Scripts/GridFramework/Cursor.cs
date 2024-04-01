using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Personal.Utilities;
using UnityEngine.Events;
using System;
using static EventBus<Event>;

public class Cursor : MonoBehaviour
{
    [SerializeField] LayerMask _layer;
    [SerializeField] float _cursorHeight = 0.5f;
    GridXZ<Cell> _grid;
    Vector3 _cursorLocation = Vector3.zero;

    MeshFilter _cursorModel;
    Renderer _cursorRenderer;
    
    Mesh _defaultCursor;
    Vector3 _defaultCursorScale;
    [SerializeField] GameObject _rangeDisplay;

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

    private void OnEnable()
    {
        EventBus<BuildingSwitchedEvent>.OnEvent += SetCursorModel;
    }

    private void OnDisable()
    {
        EventBus<BuildingSwitchedEvent>.OnEvent -= SetCursorModel;
    }
    public void SetCursorDefault()
    {    
        _cursorModel.mesh = _defaultCursor;
        _cursorModel.transform.localScale = _defaultCursorScale;
        _rangeDisplay.SetActive(false);

    }
    public void SetCursorModel(BuildingSwitchedEvent buildingSwitchedEvent)
    {
        if (buildingSwitchedEvent.buildingType != null)
        {
            _cursorModel.mesh = Search.FindComponentInChildrenWithTag<Transform>(buildingSwitchedEvent.buildingType.prefab, "TowerMesh").GetComponent<MeshFilter>().sharedMesh;
            _cursorModel.transform.localScale = new Vector3(2, 2, 2);
            _rangeDisplay.SetActive(true);
            _rangeDisplay.transform.localScale = new Vector3(buildingSwitchedEvent.buildingType.range, _rangeDisplay.transform.localScale.y, buildingSwitchedEvent.buildingType.range);
        }            
        else
        {
            _rangeDisplay.SetActive(false);
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
