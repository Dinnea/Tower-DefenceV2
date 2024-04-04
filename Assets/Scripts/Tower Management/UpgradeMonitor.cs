using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Personal.GridFramework;

public class UpgradeMonitor : MonoBehaviour
{
    GridXZ<Cell> _grid;
    private void Start()
    {
        _grid = GetComponent<GridManager>().GetGrid();
    }

    /// <summary>
    /// If the cell is taken, check if the player can afford the tower upgrade. 
    /// Display an indicating graphic if yes. If no, or if there is no upgrade option, hide the graphic.
    /// </summary>
    /// <param name="money"></param>
    private void checkUpgradePossible(float money)
    {
        if (_grid!=null)
        {
            for (int x = 0; x < _grid.GetWidthInColumns(); x++)
            {
                for (int z = 0; z < _grid.GetHeightInRows(); z++)
                {
                    Cell cell = _grid.GetCellContent(x, z);
                    if (!cell.IsCellFree())
                    {
                        BuildingTypeSO temp = cell.GetObjectOnTileType().upgrade;
                        if (temp != null && temp.cost <= money)
                        {
                            cell.GetObjectOnTile().GetComponentInChildren<Image>().enabled = true;//enable upgrade arrow
                        }
                        else
                        {
                            cell.GetObjectOnTile().GetComponentInChildren<Image>().enabled = false;
                        }
                    }
                    
                }
            }
        }
    }

    private void OnEnable()
    {
       MoneyManager.onMoneyChanged += checkUpgradePossible;
    }
    private void OnDisable()
    {
        MoneyManager.onMoneyChanged -= checkUpgradePossible;
    }
}
