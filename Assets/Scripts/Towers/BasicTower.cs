using System.Collections;
using Assets.Scripts.Bullets;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class BasicTower : TowerController
    {
        protected override IEnumerator Fire()
        {
            while (Targetter.HasActiveTarget())
            {            
                CurrentTarget = Targetter.FindTarget(AttackType);
                if (CurrentTarget == null)
                {
                    Debug.Log("Cur target null");
                    yield return null;
                }
                var direction = CurrentTarget.transform.position - transform.position;
                //PrepareBullet(transform.position, direction, BulletMovementType.Straight);
                //PrepareBullet(transform.position, currentTarget.transform, BulletMovementType.Straight); // for follow type, fix it later
                PrepareBullet(transform.position, CurrentTarget.transform, BulletMovementType.Follow); // for follow type, fix it later

                yield return new WaitForSeconds(1 / Tower.AttackSpeed);
            } 
            IsAtacking = false;

        }
    }
}

