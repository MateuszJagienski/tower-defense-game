using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//enemyID, quantity, time between objects in single part of one wave and time between each part of wave
public class Wave 
{
    public WaveData[] waveData;
    private int test;
    private List<Dictionary<int, float>> completeWave;


}

[Serializable]
public class PartWave
{
    public int quantity;
    public float timeBetweenSpawn;
    public int enemyID;
}
