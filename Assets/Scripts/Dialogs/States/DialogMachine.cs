using System.Collections.Generic;
using Dialogs.Nodes;

namespace Dialogs.States
{
    public class DialogMachine : IDialogMachine
    {
        public IDialogState GetCurrentState => _current;

        private readonly DialogController _controller;
        private readonly List<DialogState> _states = new List<DialogState>();
        private IDialogState _current;
        private DialogBox _box;

        public DialogMachine(DialogBox box, DialogController controller)
        {
            _controller = controller;
            _box = box;
            _states.AddRange(new DialogState[]
            {
                new InitDialogState(this, box),
                new StartPhraseState(this, box),
                new TypePhraseState(this, box),
                new WaitDialogState(this, box),
                new CloseDialogState(this, box)
            });
        }

        public void StartDialog()
        {
            ChangeState<InitDialogState>();
        }
        
        public void ChangeState<TState>() 
            where TState : DialogState
        {
            if(_current is IExitState exit)
                exit.Exit();
            _current = _states.Find(x => x is TState);
            if(_current is IEnterState enter)
                enter.Enter();
        }

        public void TypeNext()
        {
            ChangeState<StartPhraseState>();
        }

        public void ForceType()
        {
            ChangeState<WaitDialogState>();
        }

        public DialogNode GetPhrase()
        {
            return _controller.GetNextNode();
        }

        public void Update()
        {
            if(_current is IUpdateState update)
                update.Update();
        }

        public void Finish()
        {
            ChangeState<CloseDialogState>();
        }
    }
}