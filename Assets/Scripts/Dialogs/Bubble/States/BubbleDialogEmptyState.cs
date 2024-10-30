using Dialogs.States;

namespace Dialogs.Sideline.States
{
    public class BubbleDialogEmptyState : BubbleDialogState, IEnterState
    {
        public BubbleDialogEmptyState(IDialogMachine machine) : base(machine)
        {
        }


        public void Enter()
        {
        }
    }
}