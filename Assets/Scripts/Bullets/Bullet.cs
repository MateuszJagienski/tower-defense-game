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
        public int Id => bulletData.Id;
        private int damage;
        public int Damage => damage;
        public BulletType BulletType = BulletType.BASIC; // todo() 

        [SerializeField]
        private BulletData bulletData;
        public static int BulletLvl = 0;
        void Start()
        {
            damage = InitialDamage;
        }
        public void TakeDamage(int damage, EnemyType enemyType)
        {
            if (this.damage > damage)
            {
                this.damage -= damage;
            }
            else
            {
                DeactivateBullet();
            }
        }
        public void DeactivateBullet()
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}