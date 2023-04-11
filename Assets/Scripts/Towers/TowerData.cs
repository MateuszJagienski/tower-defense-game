using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    [CreateAssetMenu(fileName = "Object Data", menuName = "Data/TowerData")]
    public class TowerData : ScriptableObject
    {
        public string TowerName;
        public float[] Range;
        public float[] AttackSpeed;
        public int[] UpgradeCost;
        public int PurchaseCost;
        public int SellCost;
        public int Id;
        public GameObject Bullet;
        public GameObject Targetter;
        public List<GameObject> TowerModels;
    }
}

