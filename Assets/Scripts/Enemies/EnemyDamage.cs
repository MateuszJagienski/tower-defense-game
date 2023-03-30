using Assets.Scripts.Bullets;

namespace Assets.Scripts.Enemies
{
    public interface EnemyDamage
    {
        void TakeDamage(int damage, BulletType bulletType);
    }
}
