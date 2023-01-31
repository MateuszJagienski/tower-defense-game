using System.Collections;
using UnityEngine;

public class ShotgunTower : TowerController
{
    private int numberOfBullets = 6;

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
            FindTarget(attackType);
            //Vector3 rotatedVector = currentTarget.transform.position - transform.position;
            for (int i = 0; i < numberOfBullets; i++)
            {
                Vector3 rotatedVector = currentTarget.transform.position - transform.position;

                var degree = -15;
                // calculate angle between bullets, 
                degree += 5 * (i + 1);

                rotatedVector = Quaternion.AngleAxis(degree, Vector3.up) * rotatedVector;
                PrepareBullet(transform.position, rotatedVector, BulletMovementType.Straight);
            }
            yield return new WaitForSeconds(1 / tower.AttackSpeed);
        }
        isAtacking = false;
    }
}