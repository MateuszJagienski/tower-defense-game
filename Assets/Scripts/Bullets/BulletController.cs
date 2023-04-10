using Assets.Scripts.Enemies;
using UnityEngine;

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
            }
        }

        public void FollowEnemy(Transform target)
        {
            transform.LookAt(target.position, Vector3.forward);
            var step = Bullet.Speed * Time.deltaTime;
            step = Mathf.Clamp01(step);

            if (target.gameObject.activeInHierarchy)
            {
                CurrentDirection = (target.position - StartedPosition).normalized;
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            }
            else
            {
                transform.position += CurrentDirection * Bullet.Speed * Time.deltaTime;
            }
            DistanceBulletCanTravel();
        }

        public void AttackInStraightDirection(Vector3 directionTarget)
        {
            var direction = directionTarget;
            transform.position += direction * Bullet.Speed * Time.deltaTime;
            DistanceBulletCanTravel();
        }

        private void DistanceBulletCanTravel()
        {
            var distance = Vector3.Distance(transform.position, StartedPosition);
            if (distance > Bullet.Range)
            {
                Bullet.DeactivateBullet();
            }
        }

        public void EnemyHit()
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
            var enemiesInSplashRange = Physics.OverlapSphere(transform.position, Bullet.SplashRange);
            return enemiesInSplashRange;
        }

        //
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
            //transform.LookAt(currentDirection);

        }

        public void SetStartedPosition(Vector3 startedPosition)
        {
            this.StartedPosition = startedPosition;
        }

/*    public void SetBulletType(BulletType bulletType)
    {
        this.bulletType = bulletType;
    }
*/

        public void SetBulletMovementType(BulletMovementType bulletMovementType)
        {
            this.BulletMovementType = bulletMovementType;
        }
    }
}