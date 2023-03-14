using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Bullets;
using Assets.Scripts.Economy;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyController : MonoBehaviour
    {
        public static event Action<EnemyController> OnEnemyDamaged;

        private int hp;
        [SerializeField]
        private List<Enemy> enemiesModels;
        private int currentActiveEnemyId;
        // current used enemy, used for access data not model
        public Enemy CurrentActiveEnemy => enemiesModels[currentActiveEnemyId];
        private Enemy currentActiveModel;
        public int CurrentWaypointIndex { get; set; }
        private EnemySpawner enemySpawner;

        private Collider colliderBullet;

        private void Start()
        {
            enemySpawner = EnemySpawner.Instance;
        }

        public void SpawnChildren()
        {
            var index = CurrentWaypointIndex;
            var enemyMovement = GetComponent<EnemyMovement>();
            var path = enemyMovement.Path;
            var prevWaypoint = enemyMovement.GetCurrentWaypoint(--index);
            var spawnDirection = (prevWaypoint - transform.position).normalized;
            var spawnPosition = transform.position + spawnDirection;
            Debug.Log($"Dir1: {spawnDirection} prevWaypoint: {prevWaypoint} transform.pos: {transform.position} currentWaypointIndex: {CurrentWaypointIndex} currentWaypointPos: {enemyMovement.GetCurrentWaypoint(index)}");
            for (var i = 0; i < currentActiveModel.NextQuantity; i++)
            {
                // check if enemy reached waypoint
                if (WaypointReached(index, prevWaypoint, spawnPosition, spawnDirection.magnitude))
                {
                    // change spawning direction
                    var waypoint = prevWaypoint;
                    prevWaypoint = enemyMovement.GetCurrentWaypoint(--index);
                    spawnDirection = (prevWaypoint - waypoint).normalized;
                    spawnPosition = waypoint;
                }
                spawnPosition.y = transform.position.y;
                SpawnSingleEnemy(path, index, spawnPosition);
                spawnPosition += spawnDirection;
            }
        }

        private EnemyController SpawnSingleEnemy(int path, int index, Vector3 spawnPosition)
        {
            var childEnemy = enemySpawner.SpawnEnemy(currentActiveModel.NextId, spawnPosition);
            Physics.IgnoreCollision(colliderBullet, childEnemy.GetComponent<Collider>());
            childEnemy.CurrentWaypointIndex = index + 1;
            childEnemy.GetComponent<EnemyMovement>().Path = path;
            return childEnemy;
        }

        private bool WaypointReached(int index, Vector3 waypoint, Vector3 spawnPosition, float range)
        {
            return index > 0 && Vector3.Distance(waypoint, spawnPosition) <= range;
        }

        private void OnDisable()
        {
            transform.localScale = Vector3.one;
            CurrentWaypointIndex = 0;
        }

        private void OnDestroy()
        {
            Debug.Log($"Enemy controller OnDestroy: {this}");
        }

        public void DeactivateEnemy()
        {
            //  ObjectPool.Add(currentActiveModel.ID, currentActiveModel.gameObject);
            enemySpawner.Release(this);

        }

        public void Kill()
        {
            EconomySystem.Instance.IncreaseGold(1);
            DeactivateEnemy();
        }

        public EnemyController ActivateEnemyById(int id)
        {
            currentActiveEnemyId = id;
            if (currentActiveModel != null)
            {
                ObjectPool.Add(currentActiveModel.Id, currentActiveModel.gameObject);
            }
            Debug.Log("curId: " + currentActiveEnemyId);
            var enemy = ObjectPool.Get(currentActiveEnemyId);
            Debug.Log("enemy pool: " + (enemy == null));
            PlaceEnemy(enemy);

            return this;
        }

        private void PlaceEnemy(GameObject enemy)
        {
            if (enemy == null)
            {
                currentActiveModel = Instantiate(enemiesModels[currentActiveEnemyId], transform.position, Quaternion.identity);
                currentActiveModel.gameObject.SetActive(true);
                currentActiveModel.transform.parent = transform;
            }
            else
            {
                enemy.TryGetComponent(out currentActiveModel);
                currentActiveModel.transform.position = transform.position;
                currentActiveModel.gameObject.SetActive(true);
                currentActiveModel.transform.parent = transform;
            }
        }

        [ContextMenu("Auto fill models")]
        void AutofillModels()
        {
            enemiesModels = Resources.LoadAll("Models/Enemies", typeof(Enemy))
                .Cast<Enemy>()
                .OrderBy(x => x.Id)
                .ToList();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<BulletController>() == null) return;
            Debug.Log("Trigger enter:");
            var bulletController = other.gameObject.GetComponent<BulletController>();
            bulletController.EnemyHit();
            colliderBullet = bulletController.gameObject.GetComponent<Collider>();
            var bullet = other.gameObject.GetComponent<Bullet>();

            var bulletTakenDamage = Math.Min(bullet.Damage, hp);
            OnEnemyDamaged?.Invoke(this);
            currentActiveModel.GetComponent<IDamageable>().TakeDamage(1);

            bullet.TakeDamage(2);
        }

        public void CallTakeDamage()
        {
            currentActiveModel.GetComponent<IDamageable>().TakeDamage(1);
        }
    }
}