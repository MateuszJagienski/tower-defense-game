using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BulletController>() != null)
        {
            Debug.Log("trigger enter");
            BulletController bulletController = other.gameObject.GetComponent<BulletController>();
            bulletController.EnemyHit();
            Bullet bullet = other.gameObject.GetComponent<Bullet>();


            enemy.TakeDamage(1);
            bullet.TakeDamage(1);
        }
    }
}