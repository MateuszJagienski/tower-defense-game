using Assets.Scripts.Bullets;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class BasicEnemyDamage : MonoBehaviour, EnemyDamage
    {
        private EnemyController enemyController;
        private Enemy enemy;
        private int enemyHp;
        [SerializeField] private new ParticleSystem particleSystem;

        void Awake()
        {
            enemy = GetComponent<Enemy>();
        }

        void OnEnable()
        {
            enemyHp = enemy.Hp;
        }

        public void TakeDamage(int damage, BulletType bulletType)
        {
            enemyController = GetComponentInParent<EnemyController>();

            if (enemy.Id == 0) enemyController.Kill();

            enemyHp -= damage;

            if (enemy.NextQuantity > 1)
            {
                enemyController.SpawnChildren();
            }
            enemyController.ActivateEnemyById(enemy.NextId);

        }
    }
}