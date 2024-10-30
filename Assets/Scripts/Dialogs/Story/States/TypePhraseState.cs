using System.Linq;
using Dialogs.Nodes;
using Infrastructure.Timers;

namespace Dialogs.States
{
    public class TypePhraseState : DialogState, IEnterState, IUpdateState, IExitState
    {
        private readonly DialogController _controller;
        private DialogNode _node;
        private Timer _typeDelay;

        public TypePhraseState(DialogMachine machine, DialogBox box, DialogController controller) : base(machine, box)
        {
            _controller = controller;
        }

        public void Enter()
        {
            _node = _controller.GetNextNode();
            _box.Speech.SetText(_node.Text, true);
            _box.SpeakerName.SetText(_node.Name, true);

            if (_node is BranchDialogNode branch)
                _box.AnswerGroup.SetData(branch.Answers.Select(x => x.Text).ToArray());
        }

        public void Update()
        {
            if(!_box.Speech.IsTyping() && !_box.SpeakerName.IsTyping())
                _machine.ChangeState<WaitDialogState>();
        }

        public void Exit()
        {
            ForceSetText(_box.Speech, _node.Text);
            ForceSetText(_box.SpeakerName, _node.Name);
        }

        private void ForceSetText(DialogTextView view, string text)
        {
            if (view.IsTyping())
                view.SetText(text);
        }
    }
}