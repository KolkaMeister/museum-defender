namespace Dialogs.States
{
    public interface IDialogState
    {
        public DialogMachine Machine { get; }
    }

    public interface IEnterState
    {
        public void Enter();
    }

    public interface IExitState
    {
        public void Exit();
    }

    public interface IUpdateState
    {
        public void Update();
    }
}