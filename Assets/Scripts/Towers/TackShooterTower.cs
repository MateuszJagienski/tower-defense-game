using System.Collections;

using UnityEngine;

class TackShooterTower : TowerController
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
            Vector3 rotatedVector = transform.forward;
            for (int i = 0; i < numberOfBullets; i++)
            {
                var degree = (360 / numberOfBullets) * i;
                rotatedVector = Quaternion.AngleAxis(degree, Vector3.up) * rotatedVector;
                PrepareBullet(transform.position, rotatedVector, BulletMovementType.Straight);
            }
            yield return new WaitForSeconds(1 / tower.AttackSpeed);
        }
        isAtacking = false;
    }
}
