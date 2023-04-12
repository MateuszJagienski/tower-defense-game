using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class ShotgunTower : TowerController
    {
        [SerializeField] private int numberOfBullets = 6;

        protected override IEnumerator Fire()
        {
            while (Targetter.HasActiveTarget())
            {
                CurrentTarget = Targetter.FindTarget(AttackType);

                //Vector3 rotatedVector = currentTarget.transform.position - transform.position;
                for (var i = 0; i < numberOfBullets && CurrentTarget != null; i++)
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