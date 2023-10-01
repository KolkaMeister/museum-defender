using System;
using Pools;
using UnityEngine;

namespace Di
{
    public class DiWW2 : DiMonoInstaller
    {
        public Transform ProjectileContainer; 
        public Bullet Bullet;
        private PoolLocator _locator;

        public override void Install()
        {
            _locator = new PoolLocator();
            Container.Bind(_locator);
        }

        public override void Initialize()
        {
            _locator.AddPool(Bullet, 20, ProjectileContainer, this);
            
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class InjectAttribute : Attribute
    {
    }
}