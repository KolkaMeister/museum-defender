using Dialogs.Nodes;
using Dialogs.States;
using Zenject;

namespace Dialogs
{
    public class DialogController : ITickable, IInitializable
    {
        private readonly DialogBox _box;
        private readonly DialogSystem _dialogSys;
        private readonly DialogMachine _machine;

        public DialogController(DialogBox box, DialogSystem dialogSys)
        {
            _box = box;
            _dialogSys = dialogSys;
            _machine = new DialogMachine(_box, this);
        }

        public void Initialize()
        {
            // Be carefully. No unsubscribe from buttons
            _box.Button.onClick.AddListener(OnClickedCallback);
            
            var list = _box.AnswerGroup.GetItems();
            for (var i = 0; i < list.Count; i++)
            {
                int localIndex = i;
                list[i].Button.onClick.AddListener(() => _dialogSys.Next(localIndex));
            }
        }

        public void Start()
        {
            _machine.StartDialog();
        }

        public DialogNode GetNextNode()
        {
            return _dialogSys.GetNextNode();
        }

        public void TypeNext()
        {
            _machine.TypeNext();
        }

        public void Finish()
        {
            _machine.Finish();
        }

        public void Tick()
        {
            _machine.Update();
        }

        private void OnClickedCallback()
        {
            if (_machine.GetCurrentState is TypePhraseState)
                _machine.ForceType();
            else
                _dialogSys.Next(-1);
        }
    }
}