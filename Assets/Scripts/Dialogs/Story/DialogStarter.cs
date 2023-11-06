using Dialogs.Nodes;
using UnityEngine;
using Zenject;

namespace Dialogs
{
    public class DialogStarter : MonoBehaviour, IInteractable
    {
        public string DialogName;

        [SerializeField] private string _desk;
        private IDialogSystem _dialogSys;
        private IDialogDataProvider _dataProvider;
        private DialogTree _tree;

        public string Description
        {
            get => _desk;
            set => _desk = value;
        }

        [Inject]
        public void Construct(IDialogSystem dialogSys, IDialogDataProvider dataSvc)
        {
            _dialogSys = dialogSys;
            _dataProvider = dataSvc;
        }

        private void Start()
        {
            _tree = _dataProvider.Find(DialogName);
        }

        public void Interact(Character obj)
        {
            if (obj.gameObject == gameObject) 
                return;
            _dialogSys.Start(_tree);
        }
    }
}