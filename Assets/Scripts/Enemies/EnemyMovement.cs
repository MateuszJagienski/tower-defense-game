using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        private EnemyController enemyController;
        private List<SingleWaypoints> waypoints;
        private Waypoints waypointsScript;

        private float traveledDistance;
        /// <summary>
        /// Responsible for equally distributed paths for enemies.
        /// </summary>
        private static int _staticPath = 0;

        public int Path;
        void Awake()
        {
            enemyController = gameObject.GetComponent<EnemyController>();
            waypointsScript = GameObject.Find("Waypoints").GetComponent<Waypoints>();
            waypoints = waypointsScript.AllWaypoints;

        }

        /*        /// <summary>
        /// todo() fix trigger when enemy broke, should trigger only on spawn, idk how to solve this
        /// </summary>
        private void OnEnable()
        {
            if (enemyController.CurrentWaypointIndex == 0)
                path = _staticPath++;
            if (_staticPath == waypoints.Count) _staticPath = 0;
        }
*/
        public void SetPath(int path)
        {
            Path = path;
        }

        public void SetPath()
        {
            Path = _staticPath++;
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
            var currentWaypoint = waypoints[Path].Waypoints[enemyController.CurrentWaypointIndex].transform.position;
            var currentPosition = transform.position;
            var distance = Vector3.Distance(new Vector3(currentPosition.x, 0, currentPosition.z), new Vector3(currentWaypoint.x, 0, currentWaypoint.z));
            if (distance < 0.1f)
            {
                enemyController.CurrentWaypointIndex++;
                if (enemyController.CurrentWaypointIndex == waypoints[Path].Waypoints.Count)
                {
                    enemyController.DeactivateEnemy();
                    return;
                }
            }
            enemyController.transform.LookAt(currentWaypoint);


            // 
            currentWaypoint = waypoints[Path].Waypoints[enemyController.CurrentWaypointIndex].transform.position;

            var step = enemyController.CurrentActiveEnemy.Speed * Time.deltaTime;
            traveledDistance += step;
            gameObject.transform.position = Vector3.MoveTowards(currentPosition, currentWaypoint, step);
        }

        public Vector3 GetCurrentWaypoint(int index)
        {
            if (index < 0) index = 0;
            return waypoints[Path].Waypoints[index].transform.position;
        }

        public float GetDistanceToEnd()
        {
            return Waypoints.WaypointsDistance[Path] - traveledDistance;
        }
    }
}
