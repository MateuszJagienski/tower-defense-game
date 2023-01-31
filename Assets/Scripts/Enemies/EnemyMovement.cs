using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Enemy enemy;
    private List<GameObject> waypoints;

    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        waypoints = GameObject.Find("Waypoints").GetComponent<Waypoints>().waypoints;
    }

    void Update()
    {
        MoveTowardsWaypoints();
    }

    void MoveTowardsWaypoints()
    {
        var currentWaypoint = waypoints[enemy.CurrentWaypointIndex].transform.position;
        var currentPosition = transform.position;
        var distance = Vector3.Distance(new Vector3(currentPosition.x, 0, currentPosition.z), new Vector3(currentWaypoint.x, 0, currentWaypoint.z));
        if (distance < 0.1f)
        {
            enemy.CurrentWaypointIndex++;
            Debug.Log(waypoints.Count + "child count");
            // 
            if (enemy.CurrentWaypointIndex == waypoints.Count)
            {
                enemy.DeactivateEnemy();
                return;
            }
        }
        currentWaypoint = waypoints[enemy.CurrentWaypointIndex].transform.position;

        var step = enemy.Speed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(currentPosition, currentWaypoint, step);
    }
}
