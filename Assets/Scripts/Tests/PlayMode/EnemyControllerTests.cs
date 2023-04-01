using System.Collections;
using Assets.Scripts.Enemies;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.Scripts.Tests.PlayMode
{
    public class EnemyControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [UnityTest]
        public IEnumerator ActivateEnemyByIdTest()
        {
            // Arrange
            var go = new GameObject();
            var en = go.AddComponent<EnemyController>();
            go.gameObject.AddComponent<Enemy>();
            int enemyModelId = 5;

            // Act
            var model = go.GetComponent<EnemyController>().ActivateEnemyById(enemyModelId);

            yield return new WaitForSeconds(5f);

            // Assert
            Assert.AreEqual(enemyModelId, model.CurrentActiveEnemy.Id);
        }
    }
}
