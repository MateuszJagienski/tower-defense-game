using Assets.Scripts.Bullets;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class BasicEnemyDamage : MonoBehaviour, EnemyDamage
    {
        private EnemyController enemyController;
        private Enemy enemy;
        private int enemyHp;

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
            Debug.Log("inside enemyDamage");

            if (enemy.Id == 0) enemyController.Kill(); // error

            //  enemyHp -= damage;

            if (enemy.NextQuantity > 1)
            {
                Debug.Log("NextQuantity > 1");
                enemyController.SpawnChildren();
            }
            enemyController.ActivateEnemyById(enemy.NextId);
            // Debug.Log($"EnemyDamage: " + damage);

        }



        private void OnDestroy()
        {

        }
    }
}