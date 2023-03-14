using System;
using System.Collections.Generic;
using Assets.Scripts.Enemies;

//enemyID, quantity, time between objects in single part of one wave and time between each part of wave
[Serializable]
public class PartWave
{
    public int Quantity;
    public float TimeBetweenSpawn;
    public int EnemyId;
}
