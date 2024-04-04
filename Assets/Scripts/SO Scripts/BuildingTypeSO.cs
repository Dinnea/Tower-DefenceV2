using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "ScriptableObjects/Buildings")]
public class BuildingTypeSO : ScriptableObject
{

    public string nameString;
    public GameObject prefab;
    public BuildingTypeSO upgrade;
    public int width;
    public int height;
    public int cost;
    public float damage;
    public float attackRate;
    public string attackType;
    public float range;
    /// <summary>
    /// </summary>
    /// <param name="corePosition"></param>
    /// <returns>All cells occupied by the Building</returns>
    public List<Vector2Int> GetCellsCovered(Vector2Int corePosition)
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>();
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                gridPositionList.Add(corePosition + new Vector2Int(x, y));
            }
        }
        return gridPositionList;
    }
    /// <summary>
    /// Sets parameters of the abstract Tower class.
    /// </summary>
    /// <param name="tower"></param>
    public void SetParameters(Tower tower)
    {
        tower.SetRange(range);
        tower.SetActionCD(attackRate);
        tower.SetDMG(damage);
    }
}
