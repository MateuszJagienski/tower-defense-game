using System.Collections;
using UnityEngine;

public class MortarTower : TowerController
{
    private int numberOfBullets = 8;

    private void Update()
    {
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
            var target = FixedTarget();
            Debug.Log(target);
            Debug.Log("inside fire blt, mort");
            PrepareBullet(transform.position, FixedTarget(), BulletMovementType.VerticallyLaunched);

            yield return new WaitForSeconds(1 / tower.AttackSpeed);
        }
        isAtacking = false;
    }

    private Vector3 FixedTarget()
    {
        return new Vector3(-3.25f, 0.25f, -7.5f);
    }
}
