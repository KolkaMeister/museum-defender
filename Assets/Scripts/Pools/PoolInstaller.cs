﻿using UnityEngine;
using Zenject;

namespace Pools
{
    public class PoolInstaller : MonoInstaller
    {
        [SerializeField] private PoolType _poolTypes;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _commonCapacity;
        [SerializeField] private Transform _commonParent;
        
        public override void InstallBindings()
        {
            var locator = new PoolLocator();
            Container.BindInterfacesAndSelfTo<PoolLocator>().FromInstance(locator).AsSingle();

            if ((_poolTypes & PoolType.Bullet) > 0)
                locator.Add(_bulletPrefab, _commonCapacity, _commonParent, Container);
        }
    }
}