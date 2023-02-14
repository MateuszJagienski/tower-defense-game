using UnityEngine;

[CreateAssetMenu(fileName = "Object Data", menuName = "Data/TowerData")]
public class TowerData : ScriptableObject
{
    public string TowerName;
    public float[] Range;
    public float[] AttackSpeed;
    public int[] UpgradeCost;
    public int PurchaseCost;
    public int SellCost;
    public int ID;
}

