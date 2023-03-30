using Assets.Scripts.Enemies;

namespace Assets.Scripts.Bullets
{
    public interface BulletDamage
    {
        void TakeDamage(int damage, EnemyType enemyType);
    }
}
