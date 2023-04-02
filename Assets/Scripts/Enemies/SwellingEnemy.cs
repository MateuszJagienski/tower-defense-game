using System;
using Assets.Scripts.Bullets;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class SwellingEnemy : MonoBehaviour, IEnemyDamage
    {
        [SerializeField] [Min(1)] private int swellTickRange;
        [SerializeField] private float swellSpeed;
        [SerializeField] private int splashDamage;

        private EnemyController enemyController;
        private int counter;


        private void OnEnable()
        {
            counter = 0;
            gameObject.transform.localScale = Vector3.one;
            enemyController = GetComponentInParent<EnemyController>();
            if (enemyController == null)
            {
                throw new Exception("enemyController is null");
            }
        }

    
        public void TakeDamage(int damage, BulletType bulletType)
        {
         //   transform.localScale += new Vector3(swellSpeed, swellSpeed, swellSpeed);
           // transform.position += new Vector3(0, swellSpeed, 0);
            if (++counter != swellTickRange) return;
            
            
            var radius = swellTickRange * swellSpeed;
            // gameObject.layer = 12; to avoid collision with itself
            var originalLayer = gameObject.layer;
            gameObject.layer = 12;
            var colliders = Physics.OverlapSphere(transform.position, radius, 1 << 13);
            gameObject.layer = originalLayer;

            foreach (var ec in colliders)
            {
                if (!ec.TryGetComponent(out EnemyController en)) return;

                en.CallTakeDamage(damage, bulletType);
            }
        }

    }
}
