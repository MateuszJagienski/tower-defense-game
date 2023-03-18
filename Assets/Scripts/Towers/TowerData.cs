using Assets.Scripts.Bullets;
using Assets.Scripts.UI;
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
        public Bullet[] Bullet;
        public Targetter Targetter;
        public GameObject[] TowerModels;
    }
}

