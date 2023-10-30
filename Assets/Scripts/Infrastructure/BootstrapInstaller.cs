using Dialogs;
using Infrastructure.Timers;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindDialogSystem();
            BindTimerManager();
        }

        private void BindTimerManager()
        {
            Container
                .BindInterfacesAndSelfTo<TimerManager>()
                .AsSingle();
        }

        private void BindDialogSystem()
        {
            Container
                .BindInstance(new DialogSystem())
                .AsSingle();
        }
    }
}