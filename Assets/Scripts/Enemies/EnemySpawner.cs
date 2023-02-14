using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPosition;

    [SerializeField]
    private Enemy[] enemyPrefabs;

    [SerializeField]
    private EnemyController enemyController;

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

    public EnemyController SpawnEnemy(int enemyID)
    {
        var enemy = Instantiate(enemyController, spawnPosition.position, Quaternion.identity);
        enemy.ActivateEnemyById(enemyID);
        return enemy;
    }

    private EnemyController GetEnemyById(int enemyID) 
    {
        return enemyController.ActivateEnemyById(enemyID);
    }
}
