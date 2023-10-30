using Dialogs.Nodes;
using UnityEngine;
using Zenject;

namespace Dialogs
{
    public class DialogStarter : MonoBehaviour, IInteractable
    {
        public string DialogName;

        [SerializeField] private string _desk;
        private DialogSystem _dialogSys;
        private DialogDataService _dataSvc;
        private DialogTree _tree;

        public string Description
        {
            get => _desk;
            set => _desk = value;
        }

        [Inject]
        public void Construct(DialogSystem dialogSys, DialogDataService dataSvc)
        {
            _dialogSys = dialogSys;
            _dataSvc = dataSvc;
        }

        private void Start()
        {
            _tree = _dataSvc.GetDialog(DialogName);
        }

        public void Interact(Character obj)
        {
            _dialogSys.Start(_tree);
        }
    }
}