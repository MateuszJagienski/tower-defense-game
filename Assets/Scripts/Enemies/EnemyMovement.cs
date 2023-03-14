using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        private EnemyController enemyController;
        private List<SingleWaypoints> waypoints;
        private int path;
        private static int _staticPath = 0;
        public int Path { get => path; set => path = value; }
        void Awake()
        {
            enemyController = gameObject.GetComponent<EnemyController>();
            waypoints  = GameObject.Find("Waypoints").GetComponent<Waypoints>().AllWaypoints;
        }

        private void OnEnable()
        {
            path = _staticPath++;
            if (_staticPath == waypoints.Count) _staticPath = 0;
        }

        void Update()
        {
            if (gameObject.activeInHierarchy)
            {
                MoveTowardsWaypoints();
            }
        
        }
    
        void MoveTowardsWaypoints()
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
            gameObject.transform.position = Vector3.MoveTowards(currentPosition, currentWaypoint, step);
        }

        public Vector3 GetCurrentWaypoint(int index)
        {
            if (index < 0) index = 0;
            return waypoints[path].Waypoints[index].transform.position;
        }
    }
}
