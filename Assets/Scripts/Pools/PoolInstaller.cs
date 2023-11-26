using UnityEngine;
using Zenject;

namespace Pools
{
    public class PoolInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private PoolType _poolTypes;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _commonCapacity;
        [SerializeField] private Transform _commonParent;
        
        public override void InstallBindings()
        {
            BindPoolInstaller();
            BindPoolLocator();
        }

        public void Initialize()
        {
            var locator = Container.Resolve<PoolLocator>();
            if ((_poolTypes & PoolType.Bullet) > 0)
                locator.Add(_bulletPrefab, _commonCapacity, _commonParent, Container);
        }

        private void BindPoolInstaller()
        {
            Container.Bind<IInitializable>().FromInstance(this);
        }

        private void BindPoolLocator()
        {
            Container.BindInterfacesAndSelfTo<PoolLocator>().AsSingle();
        }
    }
}