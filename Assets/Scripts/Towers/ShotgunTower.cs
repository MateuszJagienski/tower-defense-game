using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class ShotgunTower : TowerController
    {
        private int numberOfBullets = 6;

        private void Update()
        {
            RemoveInactiveTargets();

            if (Targetter.GetTargets().Count > 0 && !IsAtacking)
            {
                IsAtacking = true;
                AttackEnemy();
            }
        }

        private void AttackEnemy()
        {
            StartCoroutine(FireBullet());
        }

        IEnumerator FireBullet()
        {
            while (Targetter.GetTargets().Count > 0)
            {
                FindTarget(AttackType);
                //Vector3 rotatedVector = currentTarget.transform.position - transform.position;
                for (var i = 0; i < numberOfBullets; i++)
                {
                    var rotatedVector = CurrentTarget.transform.position - transform.position;

                    var degree = -15;
                    // calculate angle between bullets, 
                    degree += 5 * (i + 1);

                    rotatedVector = Quaternion.AngleAxis(degree, Vector3.up) * rotatedVector;
                    PrepareBullet(transform.position, rotatedVector);
                }
                yield return new WaitForSeconds(1 / Tower.AttackSpeed);
            }
            IsAtacking = false;
        }
    }
}