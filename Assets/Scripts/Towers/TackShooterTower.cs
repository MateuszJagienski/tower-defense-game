using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class TackShooterTower : TowerController
    {
        [SerializeField] private int numberOfBullets = 8;

        protected override IEnumerator Fire()
        {
            while (Targetter.HasActiveTarget())
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
