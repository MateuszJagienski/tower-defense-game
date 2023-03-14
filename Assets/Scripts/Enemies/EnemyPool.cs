using UnityEngine.Pool;

namespace Assets.Scripts.Enemies
{
    public class EnemyPool
    {
        IObjectPool<EnemyController> pool;

        //public IObjectPool<EnemyController> Pool
        //{
/*        get
        {
            if (pool == null)
            {
                pool = new ObjectPool<EnemyController>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
            }
        }*/
        // }

    }
}