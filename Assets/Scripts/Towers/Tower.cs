using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public String TowerName { get => towerData.TowerName; }
    public int PurchaseCost { get => towerData.PurchaseCost; }
    public int SellCost { get => towerData.SellCost + 100 * TowerLvl; }
    public float Range { get => towerData.Range[TowerLvl]; }
    public float AttackSpeed { get => towerData.AttackSpeed[TowerLvl]; }
    public int UpgradeCost { get => towerData.UpgradeCost[TowerLvl]; }
    public int ID { get => towerData.ID; }

    public int TowerLvl = 0;
    [SerializeField]
    private TowerData towerData;

}