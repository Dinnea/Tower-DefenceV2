using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[ExecuteInEditMode]

public class GridManager : MonoBehaviour
{
    [SerializeField] int _columns = 20;
    [SerializeField] int _rows = 15;
    [SerializeField] int _cellSize = 5;
    [SerializeField] Vector3 _origin = Vector3.zero;
    

    GridXZ<Cell> _grid;
    Mesh _mesh;
    MeshFilter _meshFilter;



    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _grid = new GridXZ<Cell>(_columns, _rows, _cellSize, _origin, (GridXZ<Cell> g, int x, int z) => new Cell(_grid, x, z));
        generateGridVisual();
    }

    //Creates a visual for grid
    void generateGridVisual()
    {
        
        _mesh = new Mesh();
        _mesh.name = "Grid";

        //arraySizes
        Vector3[] vertices = new Vector3[_rows * _columns * 4];
        int[] triangles = new int[_rows * _columns * 6];
        Vector2[] uv = new Vector2[_rows * _columns * 4];

        //set tracker indices
        int v = 0; int t = 0; int u = 0;

        for (int x = 0; x < _columns; x++)
        {
            for (int z = 0; z < _rows; z++)
            {
                Vector3 cellOffset = new Vector3(x*_cellSize, 0, z*_cellSize);
                //populate the arrays
                vertices[v] = new Vector3(_origin.x, 0, _origin.z) + cellOffset;
                vertices[v  +1] = new Vector3(_origin.x, 0, _cellSize) + cellOffset;
                vertices[v + 2] = new Vector3(_cellSize, 0, _origin.z) + cellOffset;
                vertices[v + 3] = new Vector3(_cellSize, 0, _cellSize) + cellOffset;


                triangles[t] = v;
                triangles[t+1] =  triangles[t+4] = v+1;
                triangles[t + 2] = triangles[t + 3] = v+2;
                triangles[t + 5] = v+3;


                uv[u] = new Vector2(0, 0);
                uv[u+1] = new Vector2(0, 1);
                uv[u+2] = new Vector2(1, 0);
                uv[u+3] = new Vector2(1, 1);

                v += 4;
                t += 6;
                u += 4;

            }
        }

        _mesh.vertices = vertices;
        _mesh.uv = uv;
        _mesh.triangles = triangles;
        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();
        _meshFilter.mesh = _mesh;
    }
    public GridXZ<Cell> GetGrid() { return _grid; }

}
