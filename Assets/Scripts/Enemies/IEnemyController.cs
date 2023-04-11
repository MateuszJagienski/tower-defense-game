using Assets.Scripts.Bullets;

namespace Assets.Scripts.Enemies
{
    public interface IEnemyController
    {
        Enemy CurrentActiveEnemy { get; }
        int CurrentWaypointIndex { get; set; }

        EnemyController ActivateEnemyByModelType(EnemyModelType enemyModelType);
        void CallTakeDamage(int damage, BulletType bulletType);
        void DeactivateEnemy();
        void Kill();
        void SpawnChildren();
        void SpawnChildren(EnemyModelType enemyModelType);
    }
}