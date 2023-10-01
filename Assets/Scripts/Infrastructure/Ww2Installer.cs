using Pools;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class Ww2Installer : MonoInstaller
    {
        public Transform ProjectileContainer; 
        public Bullet Bullet;

        public override void InstallBindings()
        {
            BindBulletPool();
        }

        private void BindBulletPool()
        {
            Container
                .BindInterfacesAndSelfTo<Pool<Bullet>>()
                .FromNew()
                .AsSingle()
                .WithArguments(Bullet, 20, ProjectileContainer);
        }
    }
}