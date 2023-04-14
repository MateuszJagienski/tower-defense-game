using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyController enemyController;

        private static ObjectPool<EnemyController> _pool;
        private static EnemySpawner _instance;

        public static EnemySpawner Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<EnemySpawner>();
                }
                return _instance;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            _pool = new ObjectPool<EnemyController>(
                () => Instantiate(enemyController, Vector3.zero, Quaternion.identity),
                enemy => enemy.gameObject.SetActive(true),
                enemy => enemy.gameObject.SetActive(false),
                enemy => Destroy(enemy.gameObject),
                false,
                500,
                1000
            );
       
        }

        public void SpawnEnemy(EnemyModelType enemyModelType)
        {
            _pool.Get(out var en);
            var enemyMovement = en.GetComponent<EnemyMovement>();
            var spawn = enemyMovement.GetCurrentWaypoint(en.CurrentWaypointIndex);
            enemyMovement.SetPath();
            en.transform.position = spawn;
            en.ActivateEnemyByModelType(enemyModelType);
        }
        public EnemyController SpawnEnemy(EnemyModelType enemyModelType, Vector3 spawnPosition)
        {
            _pool.Get(out var en);
            en.transform.position = spawnPosition;
            en.ActivateEnemyByModelType(enemyModelType);
            return en;
        }


        public void Release(EnemyController enemyController)
        {
            _pool.Release(enemyController);
        }

        public void SpawnTest()
        {
            SpawnEnemy(EnemyModelType.SlowedWhite);
        }
    }
}
