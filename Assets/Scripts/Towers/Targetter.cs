using Assets.Scripts.Enemies;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class Targetter : MonoBehaviour
    {
        [SerializeField] private List<GameObject> targets = new List<GameObject>();
        [SerializeField] private Tower tower;
        public List<GameObject> Targets => targets;


        public void SetRange(float radius)
        {
            GetComponent<SphereCollider>().radius = radius;
        }

        public GameObject FindClosestEnemy()
        {
            GameObject closestEnemy = null;
            var closestDistance = float.MaxValue;

            foreach (var enemy in targets)
            {
                var distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (!(distance < closestDistance)) continue;
                closestEnemy = enemy;
                closestDistance = distance;
            }

            return closestEnemy;
        }

/*        public GameObject FindFirstEnemy(List<GameObject> enemies)
        {
            var currentMax = 0;
            GameObject currentFirst = null;
            var currentClosestDistance = 999f;
            foreach (var e in enemies)
            {
                var enemy = e.GetComponent<EnemyController>();

                if (enemy.CurrentWaypointIndex >= currentMax && e.activeInHierarchy)
                {
                    currentMax = enemy.CurrentWaypointIndex;
                    var dist = Vector3.Distance(enemy.transform.position, waypoints[enemy.CurrentWaypointIndex].transform.position);
                    if (dist < currentClosestDistance)
                    {
                        currentClosestDistance = dist;
                        currentFirst = e;
                    }
                }

            }
            return currentFirst;
        }*/

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

/*        public GameObject FindLastEnemy(List<GameObject> enemies)
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
        }*/


        private void OnTriggerEnter(Collider other)
        {
            targets.Add(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (targets.Contains(other.gameObject))
            {
                targets.Remove(other.gameObject);
            }
        }
    }
}
