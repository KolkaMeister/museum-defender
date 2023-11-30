using Dialogs;
using Dialogs.Sideline;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class DialogInstaller : MonoInstaller
    {
        [SerializeField] private DialogBox _dialogBox;
        [SerializeField] private DialogSo _dialogSo;
        [SerializeField] private DialogConfigSo _dialogConfig;
        
        public override void InstallBindings()
        {
            BindDialogConfig();
            BindDialogBox();
            BindDialogDataService();
            BindDialogController();
            BindInitialization();
            
            BindDialogPicker();
            BindBubbleDialogManager();
        }

        private void BindDialogConfig()
        {
            Container
                .BindInstance(_dialogConfig)
                .AsSingle();
        }

        private void BindDialogPicker()
        {
            Container
                .Bind<IDialogPicker>()
                .To<DialogPicker>()
                .AsSingle();
        }

        private void BindBubbleDialogManager()
        {
            Container
                .Bind<IBubbleDialogManager>()
                .To<BubbleDialogManager>()
                .AsSingle();
        }

        private void BindDialogBox()
        {
            Container
                .BindInstance(_dialogBox)
                .AsSingle();
        }

        private void BindInitialization()
        {
            Container
                .Bind<IInitializable>()
                .To<DialogInitializer>()
                .AsCached();
        }

        private void BindDialogController()
        {
            Container
                .BindInterfacesAndSelfTo<DialogController>()
                .AsSingle();
        }

        private void BindDialogDataService()
        {
            Container
                .BindInterfacesAndSelfTo<DialogDataProvider>()
                .FromInstance(new DialogDataProvider(_dialogSo))
                .AsSingle();
        }
    }
}