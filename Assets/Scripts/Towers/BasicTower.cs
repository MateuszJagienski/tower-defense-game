using System.Collections;
using Assets.Scripts.Bullets;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class BasicTower : TowerController
    {
        [SerializeField] private Transform[] lanuchers;
        [SerializeField] private BulletMovementType bulletMovementType;

        protected override IEnumerator Fire()
        {
            while (Targetter.HasActiveTarget())
            {            
                CurrentTarget = Targetter.FindTarget(AttackType);
                for (var i = 0; i < lanuchers.Length; i++)
                {
                    PrepareBullet(lanuchers[i].position, CurrentTarget.transform, bulletMovementType);
                    PrepareBullet(lanuchers[i].position, CurrentTarget.transform, bulletMovementType);
                }

                yield return new WaitForSeconds(1 / Tower.AttackSpeed);
            } 
            IsAtacking = false;

        }
    }
}

