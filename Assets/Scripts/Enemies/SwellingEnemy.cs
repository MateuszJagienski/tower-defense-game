using Assets.Scripts.Bullets;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class SwellingEnemy : MonoBehaviour, IEnemyDamage
    {
        private int counter;
        [SerializeField]
        [Min(1)]
        private int swellRange;
        [SerializeField]
        private float swellSpeed;
        [SerializeField]
        private int damage;
        private EnemyController enemyController;


        void Start()
        {
            enemyController = GetComponentInParent<EnemyController>();
            if (enemyController == null)
            {
                Debug.Log("enemycontroller null in Start()");
            }
        }

    
        public void TakeDamage(int damage, BulletType bulletType)
        {
            if (enemyController == null)
            {
                enemyController = gameObject.GetComponentInParent<EnemyController>();
                Debug.Log($"enemy controller null {enemyController.CurrentActiveEnemy}");
                //enemyController.transform.localScale += new Vector3(swellSpeed, swellSpeed, swellSpeed);
            }
            if (++counter == swellRange)
            {
                counter = 0;
                var radius = swellRange * swellSpeed;
                gameObject.layer = 12;
                var colliders = Physics.OverlapSphere(transform.position, radius, 1 << 13);
                gameObject.layer = 13;

                foreach (var collider in colliders)
                {
                    var enemyControllerColider = collider.GetComponent<EnemyController>();

                    var n = collider.gameObject.GetComponent<IEnemyDamage>();
                    Debug.Log($"enmy conroller: {enemyControllerColider}");
                    if (enemyControllerColider != null)
                    {
                        enemyControllerColider.CallTakeDamage(damage, bulletType);
                    }
                }
                //enemyController.ActivateEnemyByModelType(6);
            }
            Debug.Log($"Swelling enemy counter: {counter}");
        }

    }
}
