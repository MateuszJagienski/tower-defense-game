using System.Collections.Generic;
using UnityEngine;

public enum TowerType { Shooter, Generator}

public class TowerController : MonoBehaviour
{
    [SerializeField]
    protected Targetter targetter;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject towerUI;

    protected bool isAtacking = false;
    protected GameObject activeBullet;

    protected Tower tower;

    [SerializeField]
    private List<GameObject> models;
    private List<GameObject> waypoints;

    protected AttackType attackType;

    [SerializeField]
    protected GameObject currentTarget;

    // Start is called before the first frame update
    public virtual void Start()
    {
        tower = GetComponent<Tower>();
        targetter.SetRange(tower.Range);
        if (models.Count > 0)
        {
            models.ForEach(x => x.SetActive(false));
            models[0].SetActive(true);
        }
        waypoints = GameObject.Find("Waypoints").GetComponent<Waypoints>().waypoints;

        //  GameUI.Instance.sellButtonClicked += SellTower;
    }

    protected void Update()
    {
 
    }

    public void RemoveInactiveTargets()
    {
        targetter.GetTargets().RemoveAll(i => i == null || !i.gameObject.activeInHierarchy);
    }


    public void PrepareBullet(Vector3 startedPostion, Transform target, BulletMovementType bulletMovementType)
    {
        if (target == null)
        {
            return;
        }
        activeBullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        activeBullet.GetComponent<BulletController>().SetStartedPosition(startedPostion);
        activeBullet.GetComponent<BulletController>().SetTarget(target);
        activeBullet.GetComponent<BulletController>().SetBulletMovementType(bulletMovementType);

    }

    public void PrepareBullet(Vector3 startedPostion, Vector3 targetPosition, BulletMovementType bulletMovementType)
    {
        activeBullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        activeBullet.GetComponent<BulletController>().SetStartedPosition(startedPostion);
        activeBullet.GetComponent<BulletController>().SetTargetPosition(targetPosition);
        activeBullet.GetComponent<BulletController>().SetBulletMovementType(bulletMovementType);

    }

    public void FindTarget(AttackType attackType)
    {
        var targets = targetter.GetTargets();
        switch (attackType)
        {
            case AttackType.First:
                currentTarget = FindFirstEnemy(targets);
                break;
            case AttackType.Last:
                currentTarget = FindLastEnemy(targets);
                break;
            case AttackType.Strong:
                currentTarget = FindStrongestEnemy(targets);
                break;
            case AttackType.Close:
                currentTarget = FindClosestEnemy(targets);
                break;
        }
    }

    public GameObject FindClosestEnemy(List<GameObject> enemies)
    {
        GameObject closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        return closestEnemy;
    }

    public GameObject FindFirstEnemy(List<GameObject> enemies)
    {
        int currentMax = 0;
        GameObject currentFirst = null;
        var currentClosestDistance = 999f;
        foreach (var e in enemies)
        {
            var enemy = e.GetComponent<Enemy>();

            if (enemy.CurrentWaypointIndex >= currentMax && e.activeInHierarchy)
            {
                currentMax = enemy.CurrentWaypointIndex;
                var dist = Vector3.Distance(enemy.transform.position, waypoints[enemy.CurrentWaypointIndex].transform.position);
                if (dist < currentClosestDistance)
                {
                    currentClosestDistance = dist;
                    currentFirst =  e;
                }
            }

        }
        return currentFirst;
    }

    public GameObject FindStrongestEnemy(List<GameObject> enemies)
    {
        GameObject strongestEnemy = null;
        var maxHp = 0.0f;
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<Enemy>().Hp > maxHp)
            {
                maxHp = enemy.GetComponent<Enemy>().Hp;
                strongestEnemy = enemy;
            }
        }
        return strongestEnemy;
    }

    public GameObject FindLastEnemy(List<GameObject> enemies)
    {
        Debug.Log("last enemy");
        int currentMin = 99;
        GameObject currentLast = null;
        var currentMaxDistance = 0f;
        foreach (var e in enemies)
        {
            var enemy = e.GetComponent<Enemy>();

            if (enemy.CurrentWaypointIndex <= currentMin && e.activeInHierarchy)
            {
                Debug.Log("current waypoint" + enemy.CurrentWaypointIndex);
                currentMin = enemy.CurrentWaypointIndex;
                var distanceToNextWaypoint = Vector3.Distance(enemy.transform.position, waypoints[enemy.CurrentWaypointIndex].transform.position);
                Debug.Log("dis to nex" + distanceToNextWaypoint);
                if (distanceToNextWaypoint > currentMaxDistance)
                {

                    currentMaxDistance = distanceToNextWaypoint;
                    currentLast = e;
                }
            }

        }
        return currentLast;
    }

    public void SellTower(Tower tower)
    {
        EconomySystem.Instance.IncreaseGold(tower.SellCost);
        gameObject.SetActive(false);
        Destroy(gameObject, 2);
        Debug.Log("selling tower");
    }

    public void UpgradeTower(Tower tower)
    {
        // load prefab
        if (tower.TowerLvl < models.Count - 1 && EconomySystem.Instance.DecreaseGold(tower.UpgradeCost))
        {
            models[tower.TowerLvl].SetActive(false);
            models[tower.TowerLvl + 1].SetActive(true);
            tower.TowerLvl++;
            targetter.SetRange(tower.Range);
        }
        // upgrade stats
        // flat stats: range, as, ad
        // abilities: piercing, explosion, splash, etc....
    }

    public void SetAttackType(AttackType attackType)
    {
        this.attackType = attackType;
    } 


}