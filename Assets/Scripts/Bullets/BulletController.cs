using UnityEngine;

public class BulletController : MonoBehaviour
{
    protected Transform target;

    protected Vector3 startedPosition;
    protected Vector3 currentTargetPosition = new Vector3(0, 0 ,0);
    protected float traveledDistance = 0;
    protected Bullet bullet;

    protected BulletMovementType bulletMovementType;

    // Start is called before the first frame update
    void Start()
    {
        bullet = GetComponent<Bullet>();
    }

    public virtual void Update()
    {
        Attack();
    }


    public void Attack()
    {
        switch (bulletMovementType)
        {
            case BulletMovementType.Follow:
                FollowEnemy(target);
                break;
            case BulletMovementType.Straight:
                AttackInStaightDirection(currentTargetPosition);
                break;
            case BulletMovementType.VerticallyLaunched:
                //
                break;
        }
    }

    public void FollowEnemy(Transform target)
    {        
        var step = bullet.Speed * Time.deltaTime;
        step = Mathf.Clamp01(step);
        if (target.gameObject == null || !target.gameObject.activeInHierarchy)
        {
            //transform.position += (currentTargetPosition - startedPosition) * bullet.Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, currentTargetPosition + 10 * (currentTargetPosition - startedPosition), step);
        }
        else
        {
            currentTargetPosition = target.position;
            transform.position = Vector3.MoveTowards(transform.position, currentTargetPosition, step);
        }
        DistanceBulletCanTravel();
    }

    public void AttackInStaightDirection(Vector3 position)
    {
        var direction = position;
        transform.position += direction * bullet.Speed * Time.deltaTime;
        DistanceBulletCanTravel();
    }

    private void DistanceBulletCanTravel()
    {
        float distance = Vector3.Distance(transform.position, startedPosition);
        if (distance > bullet.Range)
        {
            bullet.DeactivateBullet();
        }
    }

    public void EnemyHit()
    {
        if (bullet.SplashRange < 0)
        {
            return;
        }
        Collider[] enemiesInSplashRange = GetEnemiesInSplashRange();

        foreach (var e in enemiesInSplashRange)
        {
            if (e.GetComponent<Enemy>() != null)
            {
                e.GetComponent<Enemy>().TakeDamage(bullet.SplashDamage);
            }
        }
    }    

    public Collider[] GetEnemiesInSplashRange()
    {
        var enemiesInSplashRange = Physics.OverlapSphere(transform.position, bullet.SplashRange);
        return enemiesInSplashRange;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        currentTargetPosition = target.position;
    }

    public void SetTargetPosition(Vector3 position)
    {
        currentTargetPosition = position;
    }

    public void SetStartedPosition(Vector3 startedPosition)
    {
        this.startedPosition = startedPosition;
    }

/*    public void SetBulletType(BulletType bulletType)
    {
        this.bulletType = bulletType;
    }
*/

    public void SetBulletMovementType(BulletMovementType bulletMovementType)
    {
        this.bulletMovementType = bulletMovementType;
    }
}
public enum BulletType
{
    Normal,
    Explosive,
    Follow,
}