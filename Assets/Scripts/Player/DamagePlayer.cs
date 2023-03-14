using Assets.Scripts.Enemies;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class DamagePlayer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Enemy>() != null)
            {
                var enemy = other.gameObject.GetComponent<Enemy>();
                PlayerHealth.Instance.TakeDamage((int) enemy.Hp);
            }
        }
    }
}
