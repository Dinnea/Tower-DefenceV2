using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Personal.Utilities;
using UnityEngine.UIElements;
using System;
using Unity.VisualScripting;

public class Builder : MonoBehaviour
{
    Cursor _cursor;
    [SerializeField] List<BuildingTypeSO> _buildings;

    private void Awake()
    {
        _cursor = FindAnyObjectByType<Cursor>();
    }

    private void OnEnable()
    {
       _cursor.onClick += clicked;
    }
    private void OnDisable()
    {
        _cursor.onClick -= clicked;
    }

    private void clicked(Cursor.ClickInfo info)
    {    
        if (info.clickedCell.CanBuild())
        {            
            Transform built = Instantiate(_buildings[0].prefab.transform, info.clickedCellWorldLoc, Quaternion.identity);
            info.clickedCell.SetObjectOnTile(built);
        }
        
    }
}
