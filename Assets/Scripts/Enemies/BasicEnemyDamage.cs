using System;
using Assets.Scripts.Bullets;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class BasicEnemyDamage : MonoBehaviour, IEnemyDamage
    {

        private EnemyController enemyController;
        private Enemy enemy;
        private int enemyHp;
        private ParticleSystem particleSystem;

        void Awake()
        {
            enemy = GetComponent<Enemy>();
            particleSystem = GetComponent<ParticleSystem>();
        }

        void OnEnable()
        {
            enemyHp = enemy.Hp;
        }

        public void TakeDamage(int damage, BulletType bulletType)
        {
            enemyController = GetComponentInParent<EnemyController>();

            switch (bulletType)
            {
                case BulletType.Basic:
                    enemyHp -= damage;
                    break;
                case BulletType.Splash:
                    enemyHp -= damage;
                    break;
                case BulletType.Pierce:
                    OnBreak();
                    break;
                case BulletType.Backward:
                    enemyController.CurrentWaypointIndex = 0;
                    break;
                case BulletType.Slow:
                    break;
                default:
                    enemyHp -= damage;
                    break;
            }

            //particleSystem.Play();
            if (enemyHp <= 0)
            {
                OnBreak();
            }

        }

        private void OnBreak()
        {
            if (enemy.EnemyModelType == EnemyModelType.Red)
            {
                enemyController.Kill();
                return;
            }
            
            if (enemy.NextQuantity > 1)
            {
                enemyController.SpawnChildren();
            }
            enemyController.ActivateEnemyByModelType(enemy.NextEnemyModelType);
        }
    }
}