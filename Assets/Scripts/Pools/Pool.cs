using System.Collections.Generic;
using Di;
using UnityEngine;

namespace Pools
{
    public class Pool<T> : IPool<T>
    where T : MonoBehaviour
    {
        private Transform _parent;
        private Queue<T> _queue;
        private T _prefab;
        private DiMonoInstaller _installer;
        private int _capacity;

        public Pool(T prefab, int capacity, Transform parent, DiMonoInstaller installer)
        {
            _capacity = capacity;
            _prefab = prefab;
            _parent = parent;
            _installer = installer;
            _queue = new Queue<T>(_capacity);
        }

        public void Instantiate()
        {
            for (var i = 0; i < _capacity; i++)
            {
                T obj = _installer.InstantiatePrefab(_prefab, _parent);
                obj.gameObject.SetActive(false);
                _queue.Enqueue(obj);
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