using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        private EnemyController enemyController;
        private List<SingleWaypoints> waypoints;
        private Waypoints waypointsScript;
        private int path;
        private float traveledDistance;
        /// <summary>
        /// Responsible for equally distributed paths for enemies.
        /// </summary>
        private static int _staticPath = 0;
        public int Path { get => path; set => path = value; }
        void Awake()
        {
            enemyController = gameObject.GetComponent<EnemyController>();
            waypointsScript = GameObject.Find("Waypoints").GetComponent<Waypoints>();
            waypoints = waypointsScript.AllWaypoints;
        }

        private void OnEnable()
        {
            path = _staticPath++;
            if (_staticPath == waypoints.Count) _staticPath = 0;
        }

        void Update()
        {
            if (!gameObject.activeInHierarchy) return;
            
            MoveTowardsWaypoints();

        }
        /// <summary>
        /// Moves the enemy towards the next waypoint based on the current waypoint index and path.
        /// </summary>
        private void MoveTowardsWaypoints()
        {
            var currentWaypoint = waypoints[path].Waypoints[enemyController.CurrentWaypointIndex].transform.position;
            var currentPosition = transform.position;
            var distance = Vector3.Distance(new Vector3(currentPosition.x, 0, currentPosition.z), new Vector3(currentWaypoint.x, 0, currentWaypoint.z));
            if (distance < 0.1f)
            {
                enemyController.CurrentWaypointIndex++;
                if (enemyController.CurrentWaypointIndex == waypoints[path].Waypoints.Count)
                {
                    enemyController.DeactivateEnemy();
                    return;
                }
            }
            enemyController.transform.LookAt(currentWaypoint);


            // 
            currentWaypoint = waypoints[path].Waypoints[enemyController.CurrentWaypointIndex].transform.position;

            var step = enemyController.CurrentActiveEnemy.Speed * Time.deltaTime;
            traveledDistance += step;
            gameObject.transform.position = Vector3.MoveTowards(currentPosition, currentWaypoint, step);
        }

        public Vector3 GetCurrentWaypoint(int index)
        {
            if (index < 0) index = 0;
            return waypoints[path].Waypoints[index].transform.position;
        }

        public float GetDistanceToEnd()
        {
            return Waypoints.WaypointsDistance[path] - traveledDistance;
        }
    }
}
