using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Pools
{
    public class PoolLocator : IInitializable
    {
        private readonly Dictionary<Type, object> _pools = new Dictionary<Type, object>();

        public void Add<T>(T prefab, int capacity, Transform parent, DiContainer container) where T : MonoBehaviour
        {
            _pools[typeof(T)] = new SimplePool<T>(prefab, capacity, parent, container);
        }

        public void Add<T>(IPool<T> pool) where T : MonoBehaviour =>
            _pools[typeof(T)] = pool;

        public IPool<T> Get<T>() where T : MonoBehaviour =>
            (IPool<T>)_pools[typeof(T)];


        public void Initialize()
        {
            foreach (var pool in _pools)
            {
                if (pool.Value is IPool ipool)
                    ipool.Initialize();
                else
                    Debug.Log(
                        $"Pool of {pool.Key} type is not inherited from {typeof(IPool)} interface. It will not initialized");
            }
        }
    }
}