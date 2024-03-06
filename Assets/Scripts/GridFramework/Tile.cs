using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile 
{
    private GridXZ<Tile> _grid;
    private int _x, _z;

    public Tile(GridXZ<Tile> grid, int x, int z)
    {
        _grid = grid;
        _x = x;
        _z = z;
    }
}
