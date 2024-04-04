using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddBuildOptions : MonoBehaviour
{
    [SerializeField] GameObject _buttonPrefab;
    List<BuildingTypeSO> list;

    /// <summary>
    /// Creates a button for every building option provided in Builder
    /// </summary>
    private void Awake()
    {
        list= GameObject.FindGameObjectWithTag("Player").GetComponent<Builder>().GetBuildOptions();
        foreach (BuildingTypeSO buildingType in list)
        {
            GameObject temp = Instantiate(_buttonPrefab, transform);
            temp.GetComponent<BuildSelector>().AssignBuildingType(buildingType);
        }
    }
}
