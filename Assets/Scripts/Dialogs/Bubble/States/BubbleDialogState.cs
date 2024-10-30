using Dialogs.States;

namespace Dialogs.Sideline.States
{
    public class BubbleDialogState : IDialogState
    {
        protected readonly IDialogMachine _machine;

        public IDialogMachine Machine => _machine;

        protected BubbleDialogState(IDialogMachine machine)
        {
            _machine = machine;
        }
    }
}