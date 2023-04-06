using UnityEngine;

namespace Assets.Scripts.Bullets
{
    public class BulletSpawner : MonoBehaviour, IBulletSpawner
    {
        public Bullet SpawnBulletAtPosition(BulletType bulletType, Vector3 position)
        {
            var bullet = BulletPool.Get(bulletType);
            if (bullet == null)
            {
                bullet = Instantiate(BulletPrefabs.GetBulletByType(bulletType), position, Quaternion.identity);
            }
            return bullet;
        }
    }
}