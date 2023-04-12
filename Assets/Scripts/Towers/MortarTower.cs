using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class MortarTower : TowerController
    {
        private int numberOfBullets = 8;

        protected override IEnumerator Fire()
        {
            while (Targetter.HasActiveTarget())
            {
                var target = FixedTarget();
                Debug.Log(target);
                Debug.Log("inside fire blt, mort");
                //   PrepareBullet(transform.position, FixedTarget(), BulletMovementType.VerticallyLaunched);

                yield return new WaitForSeconds(1 / Tower.AttackSpeed);
            }
            IsAtacking = false;
        }

        private Vector3 FixedTarget()
        {
            return new Vector3(-3.25f, 0.25f, -7.5f);
        }
    }
}
