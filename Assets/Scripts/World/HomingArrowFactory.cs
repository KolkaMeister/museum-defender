using JetBrains.Annotations;
using UnityEngine;
using World;
using Zenject;

namespace Infrastructure
{
    [UsedImplicitly]
    public class HomingArrowFactory : IArrowFactory
    {
        private readonly HomingArrow _prefab;
        private readonly DiContainer _container;

        public HomingArrowFactory(HomingArrow prefab, DiContainer container)
        {
            _prefab = prefab;
            _container = container;
        }
        
        public HomingArrow Create(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            var instance = _container.InstantiatePrefab(_prefab, position, rotation, parent).GetComponent<HomingArrow>();
            return instance;
        }
    }

    public interface IArrowFactory
    {
        public HomingArrow Create(Vector3 position, Quaternion rotation, Transform parent = null);
    }
}