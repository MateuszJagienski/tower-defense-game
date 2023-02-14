using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyController enemyController;
    private List<GameObject> waypoints;

    void Start()
    {
        enemyController = gameObject.GetComponent<EnemyController>();
        waypoints = GameObject.Find("Waypoints").GetComponent<Waypoints>().waypoints;
    }

    void Update()
    {
        MoveTowardsWaypoints();
    }

    void MoveTowardsWaypoints()
    {
        var currentWaypoint = waypoints[enemyController.CurrentWaypointIndex].transform.position;
        var currentPosition = transform.position;
        var distance = Vector3.Distance(new Vector3(currentPosition.x, 0, currentPosition.z), new Vector3(currentWaypoint.x, 0, currentWaypoint.z));
        if (distance < 0.1f)
        {
            enemyController.CurrentWaypointIndex++;
            // 
            if (enemyController.CurrentWaypointIndex == waypoints.Count)
            {
                enemyController.DeactivateEnemy();
                return;
            }
        }
        currentWaypoint = waypoints[enemyController.CurrentWaypointIndex].transform.position;
        // nie dziala dla enemyID = 6, chyba?
        var step = enemyController.CurrentActiveEnemy.Speed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(currentPosition, currentWaypoint, step);
    }
}
