using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Bullets;
using Assets.Scripts.Economy;
using Assets.Scripts.Enemies;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public enum TowerType { Shooter, Generator}

    public class TowerController : MonoBehaviour
    {
        [SerializeField]
        protected Targetter Targetter;
        [SerializeField]
        private GameObject bulletPrefab;
        [SerializeField]
        private GameObject towerUi;

        protected bool IsAtacking = false;
        protected GameObject ActiveBullet;

        protected Tower Tower;

        [SerializeField]
        private List<GameObject> models;
        private List<Transform> waypoints;

        protected AttackType AttackType;

        [SerializeField]
        protected GameObject CurrentTarget;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            Tower = GetComponent<Tower>();
            Targetter.SetRange(Tower.Range);
            if (models.Count > 0)
            {
                models.ForEach(x => x.SetActive(false));
                models[0].SetActive(true);
            }
            //  waypoints = GameObject.Find("Waypoints").GetComponent<Waypoints>().waypoints;
            waypoints = GameObject.Find("Waypoints").GetComponent<Waypoints>().GetFlattenListOfAllWaypoints();

            //  GameUI.Instance.sellButtonClicked += SellTower;
        }

        protected void Update()
        {
        }

        public void PrepareBullet(Vector3 startedPostion, Transform target, BulletMovementType bulletMovementType)
        {
            if (target == null)
            {
                return;
            }
            ActiveBullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, 1.282309f, transform.position.z), Quaternion.LookRotation(target.position));
            ActiveBullet.GetComponent<BulletController>().SetStartedPosition(startedPostion);
            ActiveBullet.GetComponent<BulletController>().SetTargetInfo(target);
            ActiveBullet.GetComponent<BulletController>().SetBulletMovementType(bulletMovementType);

        }

        // requiers target direction vector, currentTarget.transform.position - transform.position;
        public void PrepareBullet(Vector3 startedPostion, Vector3 targetDirection)
        {
            ActiveBullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, 1.282309f, transform.position.z), Quaternion.identity);
            ActiveBullet.GetComponent<BulletController>().SetStartedPosition(startedPostion);

            ActiveBullet.GetComponent<BulletController>().SetBulletMovementType(BulletMovementType.Straight);
            ActiveBullet.GetComponent<BulletController>().SetShotDirection(targetDirection);

        }
        
        public void RemoveInactiveTargets()
        {
            Targetter.Targets.RemoveAll(i => i == null || !i.activeInHierarchy);
        }

        public void FindTarget(AttackType attackType)
        {
            var targets = Targetter.Targets;
            switch (attackType)
            {
                case AttackType.First:
                    CurrentTarget = FindFirstEnemy(targets);
                    break;
                case AttackType.Last:
                    CurrentTarget = FindLastEnemy(targets);
                    break;
                case AttackType.Strong:
                    CurrentTarget = FindStrongestEnemy(targets);
                    break;
                case AttackType.Close:
                    CurrentTarget = FindClosestEnemy(targets);
                    break;
            }
        }

        public GameObject FindClosestEnemy(List<GameObject> enemies)
        {
            GameObject closestEnemy = null;
            var closestDistance = float.MaxValue;

            foreach (var enemy in enemies)
            {
                var distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestEnemy = enemy;
                    closestDistance = distance;
                }
            }

            return closestEnemy;
        }

        public GameObject FindFirstEnemy(List<GameObject> enemies)
        {
            GameObject currentFirst = null;
            var currentFirstDistance = float.MaxValue;
            foreach (var target in enemies)
            {
                var enemyMovement = target.GetComponent<EnemyMovement>();
                var dist = enemyMovement.GetDistanceToEnd();

                if (!target.activeInHierarchy || !(dist < currentFirstDistance)) continue;

                currentFirstDistance = dist;
                currentFirst = target;
            }
            return currentFirst;
        }

        public GameObject FindStrongestEnemy(List<GameObject> enemies)
        {
            GameObject strongestEnemy = null;
            var maxId = 0.0f;
            foreach (var enemy in enemies.Where(enemy => enemy.GetComponent<Enemy>().Id > maxId))
            {
                maxId = enemy.GetComponent<Enemy>().Id;
                strongestEnemy = enemy;
            }
            
            return strongestEnemy;
        }

        public GameObject FindLastEnemy(List<GameObject> enemies)
        {
            Debug.Log("last enemy");
            var currentMin = 99;
            GameObject currentLast = null;
            var currentMaxDistance = 0f;
            foreach (var e in enemies)
            {
                var enemy = e.GetComponent<EnemyController>();

                if (enemy.CurrentWaypointIndex <= currentMin && e.activeInHierarchy)
                {
                    Debug.Log("current waypoint" + enemy.CurrentWaypointIndex);
                    currentMin = enemy.CurrentWaypointIndex;
                    var distanceToNextWaypoint = Vector3.Distance(enemy.transform.position, waypoints[enemy.CurrentWaypointIndex].transform.position);
                    Debug.Log("dis to nex" + distanceToNextWaypoint);
                    if (distanceToNextWaypoint > currentMaxDistance)
                    {

                        currentMaxDistance = distanceToNextWaypoint;
                        currentLast = e;
                    }
                }

            }
            return currentLast;
        }

        public void SellTower(Tower tower)
        {
            EconomySystem.Instance.IncreaseGold(tower.SellCost);
            gameObject.SetActive(false);
            Destroy(gameObject, 2);
            Debug.Log("selling tower");
        }

        public void UpgradeTower(Tower tower)
        {
            // load prefab
            if (tower.TowerLvl < models.Count - 1 && EconomySystem.Instance.DecreaseGold(tower.UpgradeCost))
            {
                models[tower.TowerLvl].SetActive(false);
                models[tower.TowerLvl + 1].SetActive(true);
                tower.TowerLvl++;
                Targetter.SetRange(tower.Range);
            }
            // upgrade stats
            // flat stats: range, as, ad
            // abilities: piercing, explosion, splash, etc....
        }

        public void SetAttackType(AttackType attackType)
        {
            this.AttackType = attackType;
        } 


    }
}