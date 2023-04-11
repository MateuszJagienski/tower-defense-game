using System;
using System.Collections.Generic;
using Assets.Scripts.Bullets;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class Tower : MonoBehaviour
    {
        public string TowerName => towerData.TowerName;
        public int PurchaseCost => towerData.PurchaseCost;
        public int SellCost => towerData.SellCost + 100 * TowerLvl;
        public float Range => towerData.Range[TowerLvl];
        public float AttackSpeed => towerData.AttackSpeed[TowerLvl];
        public int UpgradeCost => towerData.UpgradeCost[TowerLvl];
        public int Id => towerData.Id;
        public GameObject Targetter => towerData.Targetter;
        public GameObject Bullet => towerData.Bullet;

        public List<GameObject> TowerModels => towerData.TowerModels;

        public int TowerLvl = 0;
        [SerializeField] private TowerData towerData;
    }
}