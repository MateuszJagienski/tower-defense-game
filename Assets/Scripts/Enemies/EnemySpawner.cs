using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPosition;

    [SerializeField]
    private Enemy[] enemyPrefabs;

    private static EnemySpawner instance;

    public static EnemySpawner Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EnemySpawner>();
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public Enemy SpawnEnemy(int enemyID)
    {
        return Instantiate(GetEnemyById(enemyID), spawnPosition.position, Quaternion.identity);
    }

    private Enemy GetEnemyById(int enemyID) 
    {
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            if (enemyPrefabs[i].ID == enemyID)
            {
                return enemyPrefabs[i];
            }
        }
        return null;
    }
}
