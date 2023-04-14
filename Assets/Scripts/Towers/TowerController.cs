using Assets.Scripts.Bullets;
using Assets.Scripts.Economy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    [RequireComponent(typeof(Tower)), RequireComponent(typeof(SphereCollider))]
    public abstract class TowerController : MonoBehaviour
    {
        protected GameObject BulletPrefab;
        protected GameObject CurrentTarget;
        protected GameObject ActiveBullet;
        protected List<GameObject> Models = new List<GameObject>();
        protected Tower Tower;
        protected ITargetter Targetter;
        protected AttackType AttackType;
        protected bool IsAtacking = false;
        
        private int towerLvl;
        private GameObject currentActiveModel;

        protected virtual void Start()
        {

            Tower = GetComponent<Tower>();
            var go = Instantiate(Tower.Targetter, transform.position, Quaternion.identity);
            go.transform.parent = transform;
            Targetter = go.GetComponent<Targetter>();
            BulletPrefab = Tower.Bullet;

            Models.AddRange(Tower.TowerModels);
            Targetter.SetRange(Tower.Range);
            towerLvl = Tower.TowerLvl;

            if (Models.Count <= 0) return;
            
            currentActiveModel = Instantiate(Models[towerLvl++], transform.position, Quaternion.identity);
            currentActiveModel.SetActive(true);
            currentActiveModel.transform.parent = transform;

        }

        protected virtual void Update()
        {
            if (!Targetter.HasActiveTarget() || IsAtacking) return;
            IsAtacking = true;
            AttackEnemy();
        }

        private void AttackEnemy()
        {
            StartCoroutine(Fire());
        }

        protected abstract IEnumerator Fire();

        protected void PrepareBullet(Vector3 startedPosition, Transform target, BulletMovementType bulletMovementType)
        {
            ActiveBullet = Instantiate(BulletPrefab, startedPosition, Quaternion.LookRotation(target.position));
            if (!ActiveBullet.TryGetComponent<BulletController>(out var bc)) return;
            
            bc.SetStartedPosition(startedPosition);
            bc.SetBulletMovementType(bulletMovementType);
            bc.SetTargetInfo(target);
        }

        // requires target direction vector, currentTarget.transform.position - transform.position;
        protected void PrepareBullet(Vector3 startedPosition, Vector3 targetDirection)
        {
            ActiveBullet = Instantiate(BulletPrefab, startedPosition, Quaternion.identity);
            if (!ActiveBullet.TryGetComponent<BulletController>(out var bc)) return;

            bc.SetStartedPosition(startedPosition);
            bc.SetBulletMovementType(BulletMovementType.Straight);
            bc.SetShotDirection(targetDirection);
        }

        public void SellTower()
        {
            EconomySystem.Instance.IncreaseGold(Tower.SellCost);
            gameObject.SetActive(false);
            Destroy(gameObject, 2);
            Debug.Log("selling tower");
        }

        public void UpgradeTower()
        {
            // load prefab
            if (towerLvl >= Models.Count || !EconomySystem.Instance.DecreaseGold(Tower.UpgradeCost)) return;

            currentActiveModel.SetActive(false);
            currentActiveModel = Instantiate(Models[towerLvl++], transform.position, Quaternion.identity);
            currentActiveModel.SetActive(true);
            currentActiveModel.transform.parent = transform;
            Targetter.SetRange(Tower.Range);
            Tower.TowerLvl++;
            // upgrade stats
            // flat stats: range, as, ad
            // abilities: piercing, explosion, splash, etc....
        }

        public void SetAttackType(AttackType attackType) => this.AttackType = attackType;
    }
}