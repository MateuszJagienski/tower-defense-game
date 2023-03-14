using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class ObjectPool
    {
        private static readonly Dictionary<int, List<GameObject>> Pool = new Dictionary<int, List<GameObject>>();

        /// <summary>
        /// Clear's the entire pool of objects, must be called on new scene load.
        /// </summary>
        public static void Clear()
        {
            Pool.Clear();
        }

        /// <summary>
        /// Returns the amount of objects in the pool of this given type.
        /// </summary>
        /// <returns></returns>
        public static int GetAmountInPool(int key)
        {
            return Pool.TryGetValue(key, out List<GameObject> poolableObjects) ? poolableObjects.Count : 0;
        }

        /// <summary>
        /// Add poolable obj to the pool.
        /// </summary>
        /// <param name="poolableObj"></param>
        public static void Add(int key, GameObject poolableObject)
        {
            List<GameObject> poolableObjects;
            if (!Pool.TryGetValue(key, out poolableObjects))
            {
                // Add to pool and deactivate enemy.
                Pool.Add(key, new List<GameObject> { poolableObject });
                poolableObject.gameObject.SetActive(false);
            }
            else
            {
                poolableObjects.Add(poolableObject);
                poolableObject.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Get available poolable obj from the pool converted to its concrete type.
        /// </summary>
        /// <returns></returns>
        public static GameObject Get(int key)
        {
            List<GameObject> poolableObjects;

            if (Pool.TryGetValue(key, out poolableObjects))
            {
                var poolableObj = poolableObjects.First();
                // Get existing enemy data
                poolableObjects.Remove(poolableObj);
                poolableObj.gameObject.SetActive(true);
                if (poolableObjects.Count == 0)
                    Pool.Remove(key);
                return poolableObj;
            }
            return null;
        }

    }
}