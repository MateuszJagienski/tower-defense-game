using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomySystem : MonoBehaviour
{
    private static EconomySystem instance;
    public static EconomySystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EconomySystem>();
            }
            return instance;
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
            if (WaveManager.Instance.IsRunning)
            {
                IncreaseGold(goldPerSecond);
            }
            else
            {
                IncreaseGold(0);
            }
            yield return new WaitForSeconds(1);
        }
    }
    public void IncreaseGold(int amount)
    {
        Gold += amount;
    }
    public bool DecreaseGold(int amount)
    {
        if (CanAfford(amount))
        {
            Gold -= amount;
            return true;
        }
        return false;
    }

    public bool CanAfford(int cost)
    {
        if (Gold >= cost) return true;
        return false;
    }
}
