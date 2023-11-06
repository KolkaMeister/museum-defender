using System.Collections;
using Dialogs;
using Infrastructure.Timers;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            BindCoroutineRunner();
            BindDialogSystem();
            BindTimerManager();
        }

        private void BindCoroutineRunner()
        {
            Container
                .Bind<ICoroutineRunner>()
                .FromInstance(this)
                .AsSingle();
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
                .BindInterfacesAndSelfTo<DialogSystem>()
                .AsSingle();
        }

        public void AbortCoroutine(IEnumerator routine)
        {
            StopCoroutine(routine);
        }

        public void RunCoroutine(IEnumerator routine)
        {
            StartCoroutine(routine);
        }
    }

    public interface ICoroutineRunner
    {
        public void AbortCoroutine(IEnumerator routine);
        public void RunCoroutine(IEnumerator routine);
    }
}