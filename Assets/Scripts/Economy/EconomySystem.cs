using System.Collections;
using Assets.Scripts.Enemies;
using UnityEngine;

namespace Assets.Scripts.Economy
{
    public class EconomySystem : MonoBehaviour
    {
        private static EconomySystem _instance;
        public static EconomySystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<EconomySystem>();
                }
                return _instance;
            }
        }
        private int goldPerSecond;
        public int Gold { get; private set; }
        void Start()
        {
            Gold = 600;
            goldPerSecond = 20;
            StartCoroutine(GenerateGold());
        }

        IEnumerator GenerateGold()
        {
            while (true)
            {
                IncreaseGold(WaveManager.Instance.IsRunning ? goldPerSecond : 0);
                yield return new WaitForSeconds(1);
            }
        }
        public void IncreaseGold(int amount)
        {
            Gold += amount;
        }
        public bool DecreaseGold(int amount)
        {
            if (!CanAfford(amount)) return false;
            Gold -= amount;
            return true;
        }

        public bool CanAfford(int cost)
        {
            return Gold >= cost;
        }
    }
}
