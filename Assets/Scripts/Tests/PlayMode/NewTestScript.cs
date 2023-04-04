using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enemies;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Arrange
        var gameObject = new GameObject();
        var enemyMovement = gameObject.AddComponent<EnemyMovement>();
        var enemyController = gameObject.AddComponent<EnemyController>();

        // Set the current waypoint index to 0, so the enemy will move towards the first waypoint
        enemyController.CurrentWaypointIndex = 0;

        // Set the current position of the enemy to the position of the first waypoint
        var firstWaypoint = enemyMovement.GetCurrentWaypoint(0);
        gameObject.transform.position = firstWaypoint;

        // Act
       // enemyMovement.MoveTowardsWaypoints();

        yield return new WaitForSeconds(5f);

        // Assert
        // Check that the enemy has moved towards the next waypoint
        var nextWaypoint = enemyMovement.GetCurrentWaypoint(1);
        Assert.AreEqual(nextWaypoint, gameObject.transform.position);

        // Check that the enemy has rotated towards the next waypoint
        var expectedRotation = Quaternion.LookRotation(nextWaypoint - firstWaypoint);
        Assert.AreEqual(expectedRotation, gameObject.transform.rotation);
    }
}
