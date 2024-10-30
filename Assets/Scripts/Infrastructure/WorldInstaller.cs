using UnityEngine;
using World;
using Zenject;

namespace Infrastructure
{
    public class WorldInstaller : MonoInstaller
    {
        [SerializeField] private WorldBound _bounds;
        [SerializeField] private HomingArrow _arrowPrefab;
        
        public override void InstallBindings()
        {
            BindArrowFactory();
            BindWorldBoundController();
        }

        private void BindWorldBoundController()
        {
            Container
                .Bind<WorldBoundController>()
                .AsSingle()
                .WithArguments(_bounds)
                .NonLazy();
        }

        private void BindArrowFactory()
        {
            Container
                .Bind<IArrowFactory>()
                .To<HomingArrowFactory>()
                .AsSingle()
                .WithArguments(_arrowPrefab);
        }
    }
}