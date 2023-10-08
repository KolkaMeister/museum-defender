using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Pools
{
    public class SimplePool<T> : IPool<T>, IPool
    where T : MonoBehaviour
    {
        private readonly Transform _parent;
        private readonly Queue<T> _queue;
        private readonly T _prefab;
        private readonly DiContainer _container;
        private readonly int _capacity;

        [Inject]
        public SimplePool(T prefab, int capacity, Transform parent, DiContainer container)
        {
            _capacity = capacity;
            _prefab = prefab;
            _parent = parent;
            _container = container;
            _queue = new Queue<T>(_capacity);
        }

        public void Initialize()
        {
            for (var i = 0; i < _capacity; i++)
            {
                var comp = _container
                    .InstantiatePrefab(_prefab, _parent)
                    .GetComponent<T>();
                comp.gameObject.SetActive(false);
                _queue.Enqueue(comp);
            }
        }

        public void Push(T obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_parent);
            _queue.Enqueue(obj);
        }

        public T Pop(Transform parent)
        {
            Transform realParent = parent ? parent : _parent;
            return Pop(realParent.position, realParent.rotation, realParent);
        }

        public T Pop(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            Transform realParent = parent ? parent : _parent;
            
            T obj = _queue.Dequeue();
            obj.transform.SetParent(realParent);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.gameObject.SetActive(true);

            return obj;
        }
    }
}