using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyPrefabs
    {
        private static readonly Dictionary<EnemyModelType, Enemy> EnemyModels;

        static EnemyPrefabs()
        {
            var list = Resources.LoadAll("Models/Enemies", typeof(Enemy))
                .Cast<Enemy>()
                .ToList();
            foreach (var e in list)
            {
                Debug.Log(e.EnemyModelType);
            }
            EnemyModels = Resources.LoadAll("Models/Enemies", typeof(Enemy))
                .Cast<Enemy>()
                .ToDictionary(e => e.EnemyData.EnemyModelType, e => e);
        }

        public static Enemy GetEnemyByType(EnemyModelType enemyModelType)
        {
            Debug.Log(enemyModelType);
            return EnemyModels[enemyModelType];
        }
    }
}

