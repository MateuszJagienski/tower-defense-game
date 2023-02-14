using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPrefabs : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> enemyPrefabs;

    public Dictionary<int, Enemy> GetEnemyPrefabs()
    {
        return enemyPrefabs
            .ToDictionary(e => e.enemyData.ID, e => e);
    }
}

