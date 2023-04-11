using Assets.Scripts.Enemies;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Towers
{

    [RequireComponent(typeof(SphereCollider))]
    public class Targetter : MonoBehaviour, ITargetter
    {
        private List<GameObject> targets = new();

        public void SetRange(float radius)
        {
            if (!TryGetComponent<SphereCollider>(out var sc)) return;
            sc.radius = radius;
        }

        public bool HasActiveTarget()
        {
            RemoveInactiveTargets();
            return targets.Count > 0;
        }

        #region Find Target

        private GameObject defaultTarget;
        /// <summary>
        /// Finds target based on attack type, FindFirst by default
        /// </summary>
        /// <param name="attackType"></param>
        /// <returns></returns>
        public GameObject FindTarget(AttackType attackType)
        {
            defaultTarget = targets[0];
            return attackType switch
            {
                AttackType.First => FindFirstEnemy(),
                AttackType.Last => FindLastEnemy(),
                AttackType.Strong => FindStrongestEnemy(),
                AttackType.Close => FindClosestEnemy(),
                _ => defaultTarget
            };

        }

        private GameObject FindClosestEnemy()
        {
            GameObject closestEnemy = defaultTarget;
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
            GameObject currentFirst = defaultTarget;
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
            GameObject strongestEnemy = defaultTarget;
            var maxId = 0;
            foreach (var enemy in targets)
            {
                /*                var enemyId = enemy.GetComponent<EnemyController>().CurrentActiveEnemy.Id;

                                if (enemyId <= maxId) continue;

                                maxId = enemyId;
                                strongestEnemy = enemy;
                */
            }

            return targets[0];
        }

        private GameObject FindLastEnemy()
        {
            GameObject currentFirst = defaultTarget;
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

        private void RemoveInactiveTargets()
        {

            targets.RemoveAll(i => i is null || !i.activeInHierarchy); // ?????????????
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
