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

        // current used enemy, used for access data not model
        public Enemy CurrentActiveEnemy => enemiesModels[currentActiveEnemyId];
        public int CurrentWaypointIndex { get; set; }
        [SerializeField] private List<Enemy> enemiesModels;

        /// <summary>
        /// Used to prevent further collinding with enemy children
        /// </summary>
        private Collider colliderBullet;
        private int currentActiveEnemyId;
        private Enemy currentActiveModel;
        private EnemySpawner enemySpawner;
        private int hp;

        private void Start()
        {
            enemySpawner = EnemySpawner.Instance;
        }

        #region Spawning enemy children
        /// <summary>
        /// Spawns enemy children based on current enemy model and NextQuantity parameter.
        /// Change spawning direction if enemy reached waypoint.
        /// </summary>
        public void SpawnChildren()
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

        /// <summary>
        /// Spawn one enemy child with inherited properties and sets IgnoreCollision to prevent being hit by the same bullet twice. 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="index"></param>
        /// <param name="spawnPosition"></param>
        /// <returns></returns>
        private EnemyController SpawnSingleEnemy(int path, int index, Vector3 spawnPosition)
        {
            var childEnemy = enemySpawner.SpawnEnemy(currentActiveModel.NextId, spawnPosition);
            Physics.IgnoreCollision(colliderBullet, childEnemy.GetComponent<Collider>());
            childEnemy.CurrentWaypointIndex = index + 1;
            childEnemy.GetComponent<EnemyMovement>().Path = path;
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
        private bool WaypointReached(int index, Vector3 waypoint, Vector3 spawnPosition, float range)
        {
            return index > 0 && Vector3.Distance(waypoint, spawnPosition) <= range;
        }

        /// <summary>
        /// Takes enemy from pool based on given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EnemyController ActivateEnemyById(int id)
        {
            currentActiveEnemyId = id;
            if (currentActiveModel != null) ObjectPool.Add(currentActiveModel.Id, currentActiveModel.gameObject);
            Debug.Log("curId: " + currentActiveEnemyId);
            var enemy = ObjectPool.Get(currentActiveEnemyId);
            Debug.Log("enemy pool: " + (enemy == null));
            PlaceEnemy(enemy);

            return this;
        }
        /// <summary>
        /// Sets parent for enemy model and sets position.
        /// </summary>
        /// <param name="enemy"></param>
        private void PlaceEnemy(GameObject enemy)
        {
            if (enemy == null)
            {
                currentActiveModel = Instantiate(enemiesModels[currentActiveEnemyId], transform.position,
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


        [ContextMenu("Auto fill models")]
        private void AutofillModels()
        {
            enemiesModels = Resources.LoadAll("Models/Enemies", typeof(Enemy))
                .Cast<Enemy>()
                .OrderBy(x => x.Id)
                .ToList();
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
            
            currentActiveModel.GetComponent<EnemyDamage>().TakeDamage(bullet.Damage, bullet.BulletType);

            bulletController.EnemyHit();
            bullet.TakeDamage(2, currentActiveModel.EnemyType);
        }

        public void CallTakeDamage(int damage, BulletType bulletType)
        {
            currentActiveModel.GetComponent<EnemyDamage>().TakeDamage(damage, bulletType);
        }
    }
}