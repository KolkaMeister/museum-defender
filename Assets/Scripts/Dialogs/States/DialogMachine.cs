using System.Collections.Generic;

namespace Dialogs.States
{
    public class DialogMachine : IDialogMachine
    {
        public IDialogState GetCurrentState => _current;

        private readonly List<IDialogState> _states = new List<IDialogState>();
        private IDialogState _current;

        public void AddState(IDialogState state)
        {
            _states.Add(state);
        }

        public void RemoveState(IDialogState state)
        {
            _states.Remove(state);
        }

        public void StartDialog()
        {
            ChangeState<InitDialogState>();
        }
        
        public void ChangeState<TState>() 
            where TState : IDialogState
        {
            if(_current is IExitState exit)
                exit.Exit();
            _current = _states.Find(x => x is TState);
            if(_current is IEnterState enter)
                enter.Enter();
        }

        public void Update()
        {
            if(_current is IUpdateState update)
                update.Update();
        }
    }
}