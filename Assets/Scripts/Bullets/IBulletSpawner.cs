using UnityEngine;

namespace Assets.Scripts.Bullets
{
    public interface IBulletSpawner
    {

        Bullet SpawnBulletAtPosition(BulletType bulletType, Vector3 position);

    }
}