using Assets.Scripts.Bullets;

namespace Assets.Scripts.Enemies
{
    public interface IEnemyDamage
    {
        void TakeDamage(int damage, BulletType bulletType);
    }
}
