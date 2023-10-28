using Dialogs;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class DialogInstaller : MonoInstaller
    {
        [SerializeField] private DialogBox _dialogBox;
        [SerializeField] private DialogSo _dialogSo;
        
        public override void InstallBindings()
        {
            BindDialogBox();
            BindDialogDataService();
            BindDialogController();
            BindInitialization();
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
                .To<Initialization>()
                .AsSingle();
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
                .BindInterfacesAndSelfTo<DialogDataService>()
                .FromInstance(new DialogDataService(_dialogSo))
                .AsSingle();
        }
    }
}