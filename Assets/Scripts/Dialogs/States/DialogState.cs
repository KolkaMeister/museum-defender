namespace Dialogs.States
{
    public abstract class DialogState : IDialogState
    {
        protected readonly DialogBox _box;
        protected readonly DialogMachine _machine;

        public DialogMachine Machine => _machine;

        protected DialogState(DialogMachine machine, DialogBox box)
        {
            _machine = machine;
            _box = box;
        }
    }
}