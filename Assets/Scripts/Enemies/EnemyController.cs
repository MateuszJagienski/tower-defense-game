using Assets.Scripts.Bullets;
using Assets.Scripts.Economy;
using System;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyController : MonoBehaviour
    {        
        public static event Action<EnemyController> OnEnemyDamaged;

        // current used enemy, used for access data not model
        public Enemy CurrentActiveEnemy => EnemyPrefabs.GetEnemyByType(currentActiveEnemyModelType);
        public int CurrentWaypointIndex { get; set; }

        /// <summary>
        /// Used to prevent further colliding with enemy children
        /// </summary>
        private Collider colliderBullet;
        private EnemyModelType currentActiveEnemyModelType;
        private Enemy currentActiveModel;
        private EnemySpawner enemySpawner;
        private int hp;

        private void Start()
        {
            enemySpawner = EnemySpawner.Instance;
        }

        #region Spawning enemy children
        private EnemyModelType emt;
        public void SpawnChildren(EnemyModelType enemyModelType)
        {
            emt = enemyModelType;
            SpawnChildren1(emt);
        }
        public void SpawnChildren()
        {
            emt = CurrentActiveEnemy.NextEnemyModelType;
            SpawnChildren1(emt);
        }
        /// <summary>
        /// Spawns enemy children based on current enemy model and NextQuantity parameter.
        /// Change spawning direction if enemy reached waypoint.
        /// </summary>
        private void SpawnChildren1(EnemyModelType enemyModelType)
        {
            var index = CurrentWaypointIndex;
            var enemyMovement = GetComponent<EnemyMovement>();
            var path = enemyMovement.Path;
            var prevWaypoint = enemyMovement.GetCurrentWaypoint(--index);
            var spawnDirection = (prevWaypoint - transform.position).normalized;
            var spawnPosition = transform.position + spawnDirection;
            for (var i = 0; i < currentActiveModel.NextQuantity; i++)
            {
                // check if enemy reached waypoint
                if (WaypointReached(index, prevWaypoint, spawnPosition, spawnDirection.magnitude))
                {
                    spawnDirection = ChangeDirection(enemyMovement, ref prevWaypoint, ref index, out spawnPosition);
                }

                spawnPosition.y = transform.position.y;
                SpawnSingleEnemy(path, index, spawnPosition, enemyModelType);
                spawnPosition += spawnDirection;
            }
        }

        private static Vector3 ChangeDirection(EnemyMovement enemyMovement, ref Vector3 prevWaypoint, ref int index, out Vector3 spawnPosition)
        {
            // change spawning direction
            var waypoint = prevWaypoint;
            prevWaypoint = enemyMovement.GetCurrentWaypoint(--index);
            var spawnDirection = (prevWaypoint - waypoint).normalized;
            spawnPosition = waypoint;
            return spawnDirection;
        }

        /// <summary>
        /// Spawn one enemy child with inherited properties and sets IgnoreCollision to prevent being hit by the same bullet twice. 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="index"></param>
        /// <param name="spawnPosition"></param>
        /// <returns></returns>
        private EnemyController SpawnSingleEnemy(int path, int index, Vector3 spawnPosition, EnemyModelType enemyModelType)
        {
            var childEnemy = enemySpawner.SpawnEnemy(enemyModelType, spawnPosition);
            Physics.IgnoreCollision(colliderBullet, childEnemy.GetComponent<Collider>());
            childEnemy.CurrentWaypointIndex = index + 1;
            childEnemy.GetComponent<EnemyMovement>().SetPath(path);
            return childEnemy;
        }

        /// <summary>
        /// Checks if position where we want to spawn enemy is too close to waypoint.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="waypoint"></param>
        /// <param name="spawnPosition"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        private static bool WaypointReached(int index, Vector3 waypoint, Vector3 spawnPosition, float range)
        {
            return index > 0 && Vector3.Distance(waypoint, spawnPosition) <= range;
        }

        /// <summary>
        /// Takes enemy from pool based on given id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enemyModelType"></param>
        /// <returns></returns>
        public EnemyController ActivateEnemyByModelType(EnemyModelType enemyModelType)
        {
            currentActiveEnemyModelType = enemyModelType;
            if (currentActiveModel != null) EnemyPool.Add(currentActiveModel.EnemyModelType, currentActiveModel.gameObject.GetComponent<Enemy>());
            var enemy = EnemyPool.Get(currentActiveEnemyModelType);
            PlaceEnemy(enemy);

            return this;
        }
        /// <summary>
        /// Sets parent for enemy model and sets position.
        /// </summary>
        /// <param name="enemy"></param>
        private void PlaceEnemy(Enemy enemy)
        {
            if (enemy == null)
            {
                currentActiveModel = Instantiate(EnemyPrefabs.GetEnemyByType(currentActiveEnemyModelType), transform.position,
                    Quaternion.identity);
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
        #endregion

        private void OnDisable()
        {
            transform.localScale = Vector3.one;
            CurrentWaypointIndex = 0;
        }

        public void DeactivateEnemy()
        {
            enemySpawner.Release(this);
        }

        public void Kill()
        {
            EconomySystem.Instance.IncreaseGold(1);
            DeactivateEnemy();
        }

        /// <summary>
        /// If bullet hits enemy send OnEnemyDamage event.
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out BulletController bulletController) || 
                !other.gameObject.TryGetComponent(out Bullet bullet) ||
                !other.gameObject.TryGetComponent(out colliderBullet)) return;

            var bulletTakenDamage = Math.Min(bullet.Damage, hp);

            OnEnemyDamaged?.Invoke(this);

            currentActiveModel.GetComponent<IEnemyDamage>().TakeDamage(bullet.Damage, bullet.BulletType);

            bullet.TakeDamage(hp, currentActiveModel.EnemyType);
        }

        public void CallTakeDamage(int damage, BulletType bulletType)
        {
            currentActiveModel.GetComponent<IEnemyDamage>().TakeDamage(damage, bulletType);
        }
    }
}