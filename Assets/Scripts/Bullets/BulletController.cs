using System;
using Assets.Scripts.Enemies;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Scripts.Bullets
{
    public class BulletController : MonoBehaviour
    {
        protected Transform Target;

        protected Vector3 StartedPosition;
        protected Vector3 CurrentDirection;
        public Vector3 CurrentTargetPosition;
        protected float TraveledDistance = 0;
        protected Bullet Bullet;

        protected BulletMovementType BulletMovementType;

        // Start is called before the first frame update
        void Start()
        {
            Bullet = GetComponent<Bullet>();
        }

        public virtual void Update()
        {
            Attack();
            if (Target == null || !Target.gameObject.activeInHierarchy) return;

        }


        public void Init()
        {
/*            //         public void SetTargetInfo(Transform target)
            {
                this.Target = target;
                CurrentDirection = (target.transform.position - StartedPosition).normalized; // ???
                CurrentDirection.y = 0;
                CurrentTargetPosition = target.transform.position;
            }

            public void SetShotDirection(Vector3 direction)
            {
                CurrentDirection = direction.normalized;
                var rotation = Quaternion.LookRotation(CurrentDirection.normalized);
                rotation *= transform.rotation;
                transform.rotation = rotation;
            }

            public void SetStartedPosition(Vector3 startedPosition)
            {
                this.StartedPosition = startedPosition;
            }

            public void SetBulletMovementType(BulletMovementType bulletMovementType)
            {
                this.BulletMovementType = bulletMovementType;
            }*/
            
        }

        public void Attack()
        {
            switch (BulletMovementType)
            {
                case BulletMovementType.Follow:
                    FollowEnemy(Target);
                    break;
                case BulletMovementType.Straight:
                    AttackInStraightDirection(CurrentDirection);
                    break;
                case BulletMovementType.VerticallyLaunched:
                    //
                    break;
                default:
                    FollowEnemy(Target);
                    break;
            }
        }

        #region Bullet Movement

        private void FollowEnemy(Transform target)
        {
            transform.LookAt(target.position, Vector3.forward);
            var step = Bullet.Speed * Time.deltaTime;
            step = Mathf.Clamp01(step);

            if (target.gameObject.activeInHierarchy)
            {
                CurrentDirection = (target.position - StartedPosition).normalized;
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            }
            else // lost target behaviour
            {

                transform.position += CurrentDirection * Bullet.Speed * Time.deltaTime;
            }
            DistanceBulletCanTravel();
        }

        private void AttackInStraightDirection(Vector3 directionTarget)
        {
            transform.position += directionTarget * Bullet.Speed * Time.deltaTime;
            DistanceBulletCanTravel();
        }

        #endregion
        private void DistanceBulletCanTravel()
        {
            var distance = Vector3.Distance(transform.position, StartedPosition);
            if (distance > Bullet.Range)
            {
                Bullet.DeactivateBullet();
            }
        }
        
        public void DealSplashDamage()
        {
            if (Bullet.SplashRange <= 0)
            {
                return;
            }
            var enemiesInSplashRange = GetEnemiesInSplashRange();

            foreach (var e in enemiesInSplashRange)
            {
                if (e.GetComponent<EnemyController>() != null)
                {
                    //e.GetComponent<EnemyController>().TakeDamage(bullet.SplashDamage);
                }
            }
        }    

        private Collider[] GetEnemiesInSplashRange()
        {
            return Physics.OverlapSphere(transform.position, Bullet.SplashRange);
        }

        public void SetTargetInfo(Transform target)
        {
            this.Target = target;
            CurrentDirection = (target.transform.position - StartedPosition).normalized; // ???
            CurrentDirection.y = 0;
            CurrentTargetPosition = target.transform.position;
        }

        public void SetShotDirection(Vector3 direction)
        {
            CurrentDirection = direction.normalized;
            var rotation = Quaternion.LookRotation(CurrentDirection.normalized);
            rotation *= transform.rotation;
            transform.rotation = rotation;
        }

        public void SetStartedPosition(Vector3 startedPosition)
        {
            this.StartedPosition = startedPosition;
        }
        
        public void SetBulletMovementType(BulletMovementType bulletMovementType)
        {
            this.BulletMovementType = bulletMovementType;
        }
    }
}