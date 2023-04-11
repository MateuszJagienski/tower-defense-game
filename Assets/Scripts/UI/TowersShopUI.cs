using System.Collections.Generic;
using Assets.Scripts.Economy;
using Assets.Scripts.Enemies;
using Assets.Scripts.Towers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class TowersShopUI : MonoBehaviour
    {

        [SerializeField] private List<TowerData> towersData;
        private TowerPlacement towerPlacement;
        // ???
        private int basicTowerId = 1;
        private int shotgunTowerId = 2;
        private int tackShooterId = 3;
        private EconomySystem economySystem;
        private WaveManager waveManager;

        // Start is called before the first frame update
        void Start()
        {
            waveManager = WaveManager.Instance;
            economySystem = EconomySystem.Instance; 
            towerPlacement = GameObject.Find("TowerPlacement").GetComponent<TowerPlacement>();
        }

        public void BasicTower()
        {
            ChangeTower(basicTowerId);
            Debug.Log("basic tower tsui");
        }

        public void ShotgunTower()
        {
            ChangeTower(shotgunTowerId);
        }

        public void TackShooterTower()
        {
            ChangeTower(tackShooterId);
        }

        public void AddGold()
        {
            EconomySystem.Instance.IncreaseGold(1000);
        }
        public void Skip()
        {
            WaveManager.Instance.SkipWave();
        }
        private void ChangeTower(int towerId)
        {
            Debug.Log("tsui change tower");
            var td = towersData.Find(it => it.Id == towerId);
            if (economySystem.CanAfford(td.PurchaseCost))
            {
                towerPlacement.ChangeTower(towerId - 1);
            }
        }
    }
}