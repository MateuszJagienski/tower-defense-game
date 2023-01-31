using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BasicTower : TowerController
{
    private void Update()
    {
        base.Update();
        RemoveInactiveTargets();

        if (targetter.GetTargets().Count > 0 && !isAtacking)
        {
            isAtacking = true;
            AttackEnemy();
        }
    }

    private void AttackEnemy()
    {
        StartCoroutine(FireBullet());
    }

    IEnumerator FireBullet()
    {
        while (targetter.GetTargets().Count > 0)
        {
            FindTarget(attackType);
            if (currentTarget == null)
            {
                yield return new WaitForSeconds(1 / tower.AttackSpeed);
            } else
            {
                PrepareBullet(transform.position, currentTarget.transform, BulletMovementType.Follow);
            }
            yield return new WaitForSeconds(1 / tower.AttackSpeed);
        }
        isAtacking = false;
    }
}

