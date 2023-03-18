using System.Collections;
using Assets.Scripts.Bullets;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class BasicTower : TowerController
    {
        private void Update()
        {
            base.Update();
            RemoveInactiveTargets();

            if (Targetter.Targets.Count > 0 && !IsAtacking)
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
            while (Targetter.Targets.Count > 0)
            {
                Targetter.FindTarget(AttackType);
                if (CurrentTarget == null)
                {
                    yield return new WaitForSeconds(1 / Tower.AttackSpeed);
                } else
                {
                    var direction = CurrentTarget.transform.position - transform.position;
                    //PrepareBullet(transform.position, direction, BulletMovementType.Straight);
                    //PrepareBullet(transform.position, currentTarget.transform, BulletMovementType.Straight); // for follow type, fix it later
                    PrepareBullet(transform.position, CurrentTarget.transform, BulletMovementType.Follow); // for follow type, fix it later
                }
                yield return new WaitForSeconds(1 / Tower.AttackSpeed);
            }
            IsAtacking = false;
        }
    }
}

