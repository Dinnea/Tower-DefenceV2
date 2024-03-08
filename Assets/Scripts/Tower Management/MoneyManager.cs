using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] float _money = 1000;
    [SerializeField]Builder _builder;
    public Action<MoneyChangedData> onMoneyChanged;

    private void Awake()
    {
        _builder = GetComponent<Builder>();
    }

    private void Start()
    {
        refreshCanAffordBuildings();
    }

    private void OnEnable()
    {
        _builder.onBuild += buyTower;
    }

    private void OnDisable()
    {
        _builder.onBuild -= buyTower;
    }

    private void buyTower(TransactionData transactionData)
    {
        _money -= transactionData.cost;
        refreshCanAffordBuildings();
    }
    private void sellTower(TransactionData transactionData)
    {
        _money += transactionData.cost;
        refreshCanAffordBuildings();
    }

    private void refreshCanAffordBuildings()
    {
        List<BuildingTypeSO> _buildOptions = _builder.GetBuildOptions();
        for (int i = 0; i < _buildOptions.Count; i++) 
        {
            onMoneyChanged?.Invoke(new MoneyChangedData(i, _money, canAfford(_buildOptions[i].cost)));
        }
    }

    private bool canAfford(float cost)
    {
        return (_money - cost) >= 0;
    }

}
public class MoneyChangedData
{
    public readonly int buildingID;
    public readonly float currentMoney;
    public readonly bool canAfford;
    public MoneyChangedData(int pBuildingID, float pCurrentMoney, bool pCanAfford)
    {
        buildingID = pBuildingID;
        currentMoney = pCurrentMoney;
        canAfford = pCanAfford;
    }
}
