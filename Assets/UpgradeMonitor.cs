using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMonitor : MonoBehaviour
{
    GridXZ<Cell> _grid;
    private void Start()
    {
        _grid = GetComponent<GridManager>().GetGrid();
    }
    private void checkUpgradePossible(float money)
    {
        if (_grid!=null)
        {
            Debug.Log("abcd");
            for (int x = 0; x < _grid.GetWidthInColumns(); x++)
            {
                for (int z = 0; z < _grid.GetHeightInRows(); z++)
                {
                    Cell cell = _grid.GetCellContent(x, z);
                    if (!cell.IsCellFree())
                    {
                        if (cell.GetObjectOnTileType().upgrade.cost <= money)
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
