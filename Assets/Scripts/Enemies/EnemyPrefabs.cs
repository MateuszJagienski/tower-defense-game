using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyPrefabs : MonoBehaviour
    {
        [SerializeField]
        private List<Enemy> enemyPrefabs;

        public Dictionary<int, Enemy> GetEnemyPrefabs()
        {
            return enemyPrefabs
                .ToDictionary(e => e.EnemyData.Id, e => e);
        }
    }
}

