using Assets.Scripts.Enemies;
using UnityEngine;

namespace Assets.Scripts.Bullets
{
    public class Bullet : MonoBehaviour, BulletDamage
    {
        public int InitialDamage => bulletData.InitialDamage[BulletLvl];
        public int SplashDamage => bulletData.SplashDamage[BulletLvl];
        public int SplashRange => bulletData.SplashRange[BulletLvl];
        public int Speed => bulletData.Speed[BulletLvl];
        public int Range => bulletData.Range[BulletLvl];
        public int Damage => damage;
        public BulletType BulletType => bulletData.BulletType;
        
        [SerializeField] private BulletData bulletData;
        
        private int damage;

        public static int BulletLvl = 0;

        private void Start()
        {
            damage = InitialDamage;
        }
        public void TakeDamage(int damage, EnemyType enemyType)
        {
            if (this.damage > damage) this.damage -= damage;
            else DeactivateBullet();
        }
        public void DeactivateBullet()
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}