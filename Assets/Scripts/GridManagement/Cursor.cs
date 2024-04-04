using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Personal.Utilities;
using UnityEngine.Events;
using System;
using Personal.GridFramework;

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

    /// <summary>
    /// Contains info on where the cursor has clicked in world location and the cell that is located there.
    /// </summary>
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
    /// <summary>
    /// Sets the cursor's mesh to the mesh of a BuildingTypeSO provided by buildingSwitchedEvent. Shows the effect range of the building.
    /// If no building is provided, reses the cursor to default.
    /// </summary>
    /// <param name="buildingSwitchedEvent"></param>
    public void SetCursorModel(BuildingSwitchedEvent buildingSwitchedEvent)
    {
        if (buildingSwitchedEvent.buildingType != null)
        {
            _cursorModel.mesh = Search.FindComponentInChildrenWithTag<Transform>(buildingSwitchedEvent.buildingType.prefab, "TowerMesh").GetComponent<MeshFilter>().sharedMesh;
            _cursorModel.transform.localScale = new Vector3(2, 2, 2);
            _rangeDisplay.SetActive(true);
            _rangeDisplay.transform.localScale = new Vector3(buildingSwitchedEvent.buildingType.range*2, _rangeDisplay.transform.localScale.y, buildingSwitchedEvent.buildingType.range*2);
        }            
        else
        {
            SetCursorDefault();
        }
    }
    /// <summary>
    /// Snaps the cursor to the grid, changes the cursor colour based on if the space is avaiable for building towers.
    /// Ensures that clickOnCell is only called when inside of the grid boundaries.
    /// </summary>
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
    /// <summary>
    /// Triggers the onClick event when LMB is clicked.
    /// </summary>
    /// <param name="targetCell"></param>
    private void clickOnCell( Cell targetCell)
    {
        if (Input.GetMouseButtonDown(0))
        {
            onClick?.Invoke(new ClickInfo(_grid.GetCellPositionInWorld(targetCell.GetX(), targetCell.GetZ()), targetCell));
        }
    }
}
