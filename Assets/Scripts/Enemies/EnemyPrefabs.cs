using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyPrefabs
    {
        private static readonly Dictionary<EnemyModelType, Enemy> EnemyModels;

        static EnemyPrefabs() =>
            EnemyModels = Resources.LoadAll("Models/Enemies", typeof(Enemy))
                .Cast<Enemy>()
                .ToDictionary(e => e.EnemyModelType, e => e);
        
        public static Enemy GetEnemyByType(EnemyModelType enemyModelType) => EnemyModels[enemyModelType];
    }
}

