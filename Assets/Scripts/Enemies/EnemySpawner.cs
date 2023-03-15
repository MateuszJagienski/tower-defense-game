using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private EnemyController enemyController;

        private ObjectPool<EnemyController> pool;
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
            pool = new ObjectPool<EnemyController>(
                () => Instantiate(enemyController, Vector3.zero, Quaternion.identity),
                enemy => enemy.gameObject.SetActive(true),
                enemy => enemy.gameObject.SetActive(false),
                enemy => Destroy(enemy.gameObject),
                false,
                500,
                1000
            );
       
        }

        public void SpawnEnemy(int enemyId)
        {
            EnemyController en;
            pool.Get(out en);
            var spawn = en.GetComponent<EnemyMovement>().GetCurrentWaypoint(en.CurrentWaypointIndex);
            en.transform.position = spawn;
            en.ActivateEnemyById(enemyId);
            //var enemy = Instantiate(enemyController, spawnPosition.position, Quaternion.identity);
            //enemy.ActivateEnemyById(enemyID);
        }
        public EnemyController SpawnEnemy(int enemyId, Vector3 spawnPosition)
        {
            EnemyController en;
            pool.Get(out en);
            en.transform.position = spawnPosition;
            en.ActivateEnemyById(enemyId);
            //var enemy = Instantiate(enemyController, spawnPosition, Quaternion.identity);
            //enemy.ActivateEnemyById(enemyID);
            //return enemy;
            return en;
        }

        public void Release(EnemyController enemyController)
        {
            pool.Release(enemyController);
        }

        private EnemyController GetEnemyById(int enemyId) 
        {
            return enemyController.ActivateEnemyById(enemyId);
        }
    }
}
