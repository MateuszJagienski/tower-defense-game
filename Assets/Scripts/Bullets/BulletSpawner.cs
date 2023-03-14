using UnityEngine;

namespace Assets.Scripts.Bullets
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform spawnPosition;

        [SerializeField]
        private Bullet[] bulletPrefabs;

        private static BulletSpawner _instance;

        public static BulletSpawner Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<BulletSpawner>();
                }
                return _instance;
            }
        }

        public Bullet SpawnBullet(int bulletId)
        {
            return Instantiate(GetBulletById(bulletId), spawnPosition.position, Quaternion.identity);
        }

        private Bullet GetBulletById(int bulletId)
        {
            for (var i = 0; i < bulletPrefabs.Length; i++)
            {
                if (bulletPrefabs[i].Id == bulletId)
                {
                    return bulletPrefabs[i];
                }
            }
            return null;
        }
    }
}
