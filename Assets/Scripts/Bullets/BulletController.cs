using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    protected Transform target;

    protected Vector3 startedPosition;
    protected Vector3 currentDirection;
    public Vector3 currentTargetPostion;
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
        // rotation toward enemy
        if (target == null || !target.gameObject.activeInHierarchy) return;
       // transform.LookAt(target.position, Vector3.forward);
    }


    public void Attack()
    {
        switch (bulletMovementType)
        {
            case BulletMovementType.Follow:
                FollowEnemy(target);
                break;
            case BulletMovementType.Straight:
                AttackInStaightDirection(currentDirection);
                break;
            case BulletMovementType.VerticallyLaunched:
                //
                break;
        }
    }

    public void FollowEnemy(Transform target)
    {
        transform.LookAt(target.position, Vector3.forward);
        var step = bullet.Speed * Time.deltaTime;
        step = Mathf.Clamp01(step);

        if (target == null || target.gameObject.activeInHierarchy)
        {
            currentDirection = (target.position - startedPosition).normalized;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
        else
        {
            transform.position += currentDirection * bullet.Speed * Time.deltaTime;
        }
        DistanceBulletCanTravel();
    }

    public void AttackInStaightDirection(Vector3 directionTarget)
    {
        Debug.Log($"dir: {directionTarget}");
        var direction = directionTarget;
        transform.position += direction * bullet.Speed * Time.deltaTime;
        Debug.Log($"trans.pos: {transform.position}");
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
        if (bullet.SplashRange <= 0)
        {
            return;
        }
        Collider[] enemiesInSplashRange = GetEnemiesInSplashRange();

        foreach (var e in enemiesInSplashRange)
        {
            if (e.GetComponent<EnemyController>() != null)
            {
                e.GetComponent<EnemyController>().TakeDamage(bullet.SplashDamage);
            }
        }
    }    

    private Collider[] GetEnemiesInSplashRange()
    {
        var enemiesInSplashRange = Physics.OverlapSphere(transform.position, bullet.SplashRange);
        return enemiesInSplashRange;
    }

    //
    public void SetTargetInfo(Transform target)
    {
        this.target = target;
        currentDirection = (target.transform.position - startedPosition).normalized; // ???
        currentDirection.y = 0;
        currentTargetPostion = target.transform.position;
    }

    public void SetShotDirection(Vector3 direction)
    {
        currentDirection = direction;
        var rotation = Quaternion.LookRotation(currentDirection.normalized);
        rotation *= transform.rotation;
        transform.rotation = rotation;
        //transform.LookAt(currentDirection);

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