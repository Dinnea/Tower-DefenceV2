using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "ScriptableObjects/Buildings")]
public class BuildingTypeSO : ScriptableObject
{

    public string nameString;
    public GameObject prefab;
    public int width;
    public int height;
    public int cost;
    public float damage;
    public float attackSpeed;
    public string attackType;
    public float range;

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
}
