using System;
using System.Collections.Generic;
using Di;
using UnityEngine;

namespace Pools
{
    public class PoolLocator
    {
        private Dictionary<Type, object> _pools = new Dictionary<Type, object>();

        public PoolLocator AddPool<T>(T prefab, int capacity, Transform parent, DiMonoInstaller installer)
            where T : MonoBehaviour
        {
            var pool = new Pool<T>(prefab, capacity, parent, installer);
            _pools.Add(typeof(T), pool);
            pool.Instantiate();
            return this;
        }

        public Pool<T> GetPool<T>()
            where T : MonoBehaviour
        {
            return (Pool<T>)_pools[typeof(T)];
        }
    }
}