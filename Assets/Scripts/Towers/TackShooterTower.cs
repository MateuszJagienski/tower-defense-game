﻿using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class TackShooterTower : TowerController
    {
        private int numberOfBullets = 8;

        private void Update()
        {
            if (Targetter.Targets.Count <= 0 || IsAtacking) return;
            IsAtacking = true;
            AttackEnemy();
        }

        private void AttackEnemy()
        {
            StartCoroutine(FireBullet());
        }

        IEnumerator FireBullet()
        {
            while (Targetter.Targets.Count > 0)
            {
                var rotatedVector = transform.forward;
                for (var i = 0; i < numberOfBullets; i++)
                {
                    var degree = (360 / numberOfBullets) * i;
                    rotatedVector = Quaternion.AngleAxis(degree, Vector3.up) * rotatedVector;
                    PrepareBullet(transform.position, rotatedVector);
                }
                yield return new WaitForSeconds(1 / Tower.AttackSpeed);
            }
            IsAtacking = false;
        }
    }
}
