using Assets.Scripts.Bullets;
using Assets.Scripts.Economy;
using Assets.Scripts.Enemies;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class TowerController : MonoBehaviour
    {
        [SerializeField] protected Targetter Targetter;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private List<GameObject> models;

        protected GameObject CurrentTarget;

        protected bool IsAtacking = false;
        protected GameObject ActiveBullet;

        protected Tower Tower;


        protected AttackType AttackType;


        // Start is called before the first frame update
        protected virtual void Start()
        {
            
            Tower = GetComponent<Tower>();
            Targetter.SetRange(Tower.Range);
            
            if (models.Count <= 0) return;

            models.ForEach(x => x.SetActive(false));
            models[0].SetActive(true);
        }

        protected void Update()
        {
        }

        public void PrepareBullet(Vector3 startedPostion, Transform target, BulletMovementType bulletMovementType)
        {
            if (target == null)
            {
                return;
            }
            ActiveBullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, 1.282309f, transform.position.z), Quaternion.LookRotation(target.position));
            ActiveBullet.GetComponent<BulletController>().SetStartedPosition(startedPostion);
            ActiveBullet.GetComponent<BulletController>().SetTargetInfo(target);
            ActiveBullet.GetComponent<BulletController>().SetBulletMovementType(bulletMovementType);

        }

        // requires target direction vector, currentTarget.transform.position - transform.position;
        public void PrepareBullet(Vector3 startedPostion, Vector3 targetDirection)
        {
            ActiveBullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, 1.282309f, transform.position.z), Quaternion.identity);
            ActiveBullet.GetComponent<BulletController>().SetStartedPosition(startedPostion);

            ActiveBullet.GetComponent<BulletController>().SetBulletMovementType(BulletMovementType.Straight);
            ActiveBullet.GetComponent<BulletController>().SetShotDirection(targetDirection);

        }
        
        public void RemoveInactiveTargets()
        {
            Targetter.Targets.RemoveAll(i => i == null || !i.activeInHierarchy);
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
            if (tower.TowerLvl >= models.Count - 1 || !EconomySystem.Instance.DecreaseGold(tower.UpgradeCost)) return;

            models[tower.TowerLvl].SetActive(false);
            models[tower.TowerLvl + 1].SetActive(true);
            tower.TowerLvl++;
            Targetter.SetRange(tower.Range);
            // upgrade stats
            // flat stats: range, as, ad
            // abilities: piercing, explosion, splash, etc....
        }

        public void SetAttackType(AttackType attackType)
        {
            this.AttackType = attackType;
        } 


    }
}