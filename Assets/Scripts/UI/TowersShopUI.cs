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

        private TowerPlacement towerPlacement;
        [SerializeField]
        private List<TowerData> towersData;
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

        void Update()
        {
            // Check if the left mouse button was clicked
            if (!Input.GetMouseButtonDown(0)) return;
            // Check if the mouse was clicked over a UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicked on the UI");
            }
        }

        public void BasicTower()
        {
            ChangeTower(basicTowerId);
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
            var td = towersData.Find(it => it.Id == towerId);
            if (economySystem.CanAfford(td.PurchaseCost))
            {
                towerPlacement.ChangeTower(towerId - 1);
            }
        }
    }
}