using System;

//enemyID, quantity, time between objects in single part of one wave and time between each part of wave
namespace Assets.Scripts.Enemies
{
    [Serializable]
    public class PartWave
    {
        public int Quantity;
        public float TimeBetweenSpawn;
        public EnemyModelType EnemyModelType;
    }
}
