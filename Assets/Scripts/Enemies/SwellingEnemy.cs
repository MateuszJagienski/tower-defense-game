using Assets.Scripts.Bullets;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class SwellingEnemy : MonoBehaviour, IEnemyDamage
    {
        [SerializeField] [Min(1)] private int swellTickRange;
        [SerializeField] private float swellSpeed;
        [SerializeField] private int splashDamage;

        [SerializeField] LayerMask layer;

        private EnemyController enemyController;
        private int counter;

        private void OnEnable()
        {
            counter = 0;
            gameObject.transform.localScale = Vector3.one;
/*            enemyController = GetComponentInParent<EnemyController>();
            if (enemyController == null)
            {
                throw new Exception("enemyController is null");
            }
*/        }

        public void TakeDamage(int damage, BulletType bulletType)
        {

            if (enemyController == null)
            {
                enemyController = GetComponentInParent<EnemyController>();
            }

            if (counter > swellTickRange)
            {
                enemyController.Kill();
                return;
            }

            transform.localScale += new Vector3(swellSpeed, swellSpeed, swellSpeed);
            transform.position += new Vector3(0, swellSpeed, 0);
            if (++counter <= swellTickRange) return;
            
            var radius = swellTickRange * swellSpeed;
            Debug.Log("s1");
            var colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (var ec in colliders)
            {
                if (!ec.TryGetComponent(out EnemyController en)) return;
                Debug.Log("s2");
                en.CallTakeDamage(damage, bulletType);
            }
        }
    }
}
