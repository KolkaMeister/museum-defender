namespace Dialogs.States
{
    public interface IDialogMachine
    {
        public void AddState(IDialogState state);
        public void RemoveState(IDialogState state);
        
        public void StartDialog();
        
        public void ChangeState<TState>()
            where TState : IDialogState;

        public void Update();
    }
}