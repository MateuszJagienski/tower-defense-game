using System.Collections;
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
                var direction = currentTarget.transform.position - transform.position;
                //PrepareBullet(transform.position, direction, BulletMovementType.Straight);
                //PrepareBullet(transform.position, currentTarget.transform, BulletMovementType.Straight); // for follow type, fix it later
                PrepareBullet(transform.position, currentTarget.transform, BulletMovementType.Follow); // for follow type, fix it later
            }
            yield return new WaitForSeconds(1 / tower.AttackSpeed);
        }
        isAtacking = false;
    }
}

