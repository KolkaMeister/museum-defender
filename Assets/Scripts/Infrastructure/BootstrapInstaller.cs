using System.Collections;
using Dialogs;
using Infrastructure.Timers;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner, IInitializable
    {
        [SerializeField] private GameObject _globalMusicPrefab;
        
        public override void InstallBindings()
        {
            BindBootstrapInstaller();
            BindCoroutineRunner();
            BindDialogSystem();
            BindTimerManager();
        }

        public void AbortCoroutine(IEnumerator routine)
        {
            StopCoroutine(routine);
        }

        public Coroutine RunCoroutine(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }

        public void Initialize()
        {
            GameObject music = Instantiate(_globalMusicPrefab);
            DontDestroyOnLoad(music);
        }

        private void BindBootstrapInstaller()
        {
            Container.Bind<IInitializable>().FromInstance(this);
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
    }
}