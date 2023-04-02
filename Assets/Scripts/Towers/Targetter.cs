using Assets.Scripts.Enemies;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class Targetter : MonoBehaviour
    {
        private List<GameObject> targets = new List<GameObject>();

        public List<GameObject> Targets => targets;

        public void SetRange(float radius)
        {
            GetComponent<SphereCollider>().radius = radius;
        }

        #region Find Target
        /// <summary>
        /// Finds target based on attack type, FindFirst by default
        /// </summary>
        /// <param name="attackType"></param>
        /// <returns></returns>
        public GameObject FindTarget(AttackType attackType)
        {
            return attackType switch
            {
                AttackType.First => FindFirstEnemy(),
                AttackType.Last => FindLastEnemy(),
                AttackType.Strong => FindStrongestEnemy(),
                AttackType.Close => FindClosestEnemy(),
                _ => FindFirstEnemy()
            };
        }

        private GameObject FindClosestEnemy()
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

        private GameObject FindFirstEnemy()
        {
            GameObject currentFirst = null;
            var currentFirstDistance = float.MaxValue;
            foreach (var target in targets)
            {
                var enemyMovement = target.GetComponent<EnemyMovement>();
                var dist = enemyMovement.GetDistanceToEnd();

                if (!target.activeInHierarchy || !(dist < currentFirstDistance)) continue;
                
                currentFirstDistance = dist;
                currentFirst = target;
            }
            return currentFirst;
        }

        /// todo() fix this
        private GameObject FindStrongestEnemy()
        {
            GameObject strongestEnemy = null;
            var maxId = 0;
            foreach (var enemy in targets)
            {
/*                var enemyId = enemy.GetComponent<EnemyController>().CurrentActiveEnemy.Id;

                if (enemyId <= maxId) continue;

                maxId = enemyId;
                strongestEnemy = enemy;
*/            }

            return targets[0];
        }

        private GameObject FindLastEnemy()
        {
            GameObject currentFirst = null;
            var currentFirstDistance = float.MinValue;
            foreach (var target in targets)
            {
                var enemyMovement = target.GetComponent<EnemyMovement>();
                var dist = enemyMovement.GetDistanceToEnd();

                if (!target.activeInHierarchy || !(dist > currentFirstDistance)) continue;

                currentFirstDistance = dist;
                currentFirst = target;
            }
            return currentFirst;
        }
        #endregion

        public void RemoveInactiveTargets()
        {
            Targets.RemoveAll(i => i == null || !i.activeInHierarchy);
        }

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
