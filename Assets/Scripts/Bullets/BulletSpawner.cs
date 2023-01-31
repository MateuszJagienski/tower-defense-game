using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPosition;

    [SerializeField]
    private Bullet[] bulletPrefabs;

    private static BulletSpawner instance;

    public static BulletSpawner Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BulletSpawner>();
            }
            return instance;
        }
    }

    public Bullet SpawnBullet(int bulletID)
    {
        return Instantiate(GetBulletById(bulletID), spawnPosition.position, Quaternion.identity);
    }

    private Bullet GetBulletById(int bulletID)
    {
        for (int i = 0; i < bulletPrefabs.Length; i++)
        {
            if (bulletPrefabs[i].ID == bulletID)
            {
                return bulletPrefabs[i];
            }
        }
        return null;
    }
}
