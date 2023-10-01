using UnityEngine;

namespace Pools
{
    public interface IPool<T>
    {
        public void Push(T obj);
        public T Pop(Transform parent);
        public T Pop(Vector3 position, Quaternion rotation, Transform parent = null);
    }
}