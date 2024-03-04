using UnityEngine;
using Zenject;

namespace Dialogs.Sideline
{
    public class BubbleDialogStarter : MonoBehaviour, IInteractable
    {
        public string TemplateName;

        [SerializeField] private string _description;
        private IDialogSystem _dialogSys;
        private IBubbleDialogManager _manager;
        private Character _character;

        public InteractionType Id { get; set; } = InteractionType.Dialog;

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        [Inject]
        public void Construct(IBubbleDialogManager manager)
        {
            _character = GetComponent<Character>();
            _manager = manager;
        }
        
        public void Interact(Character obj)
        {
            if (obj.gameObject == gameObject || obj.Id == EntityType.Player) 
                return;
            _manager.CreateDialog(obj, _character, TemplateName);
        }
    }
}