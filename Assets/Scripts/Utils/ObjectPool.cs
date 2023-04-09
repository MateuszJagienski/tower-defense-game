using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public abstract class ObjectPool<TKey, TValue> where TValue : Component 
    {
        private static readonly Dictionary<TKey, List<TValue>> Pool = new Dictionary<TKey, List<TValue>>();

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
        public static int GetAmountInPool(TKey key)
        {
            return Pool.TryGetValue(key, out List<TValue> poolableObjects) ? poolableObjects.Count : 0;
        }

        /// <summary>
        /// Add poolable obj to the pool and deactivate it.
        /// </summary>
        /// <param name="poolableObj"></param>
        public static void Add(TKey key, TValue poolableObject)
        {
            if (!Pool.TryGetValue(key, out var poolableObjects))
            {
                // Add to pool and deactivate enemy.
                Pool.Add(key, new List<TValue> { poolableObject });
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
        public static TValue Get(TKey key)
        {
            if (!Pool.TryGetValue(key, out var poolableObjects)) return null;

            var poolableObj = poolableObjects.First();

            // Get existing enemy data
            poolableObjects.Remove(poolableObj);
            poolableObj.gameObject.SetActive(true);
            if (poolableObjects.Count == 0)
                Pool.Remove(key);
            return poolableObj;
        }
    }
}