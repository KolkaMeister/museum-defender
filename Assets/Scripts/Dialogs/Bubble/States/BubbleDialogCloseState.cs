using Dialogs.States;

namespace Dialogs.Sideline.States
{
    public class BubbleDialogCloseState : BubbleDialogState, IEnterState
    {
        private readonly IBubbleDialogController _controller;

        public BubbleDialogCloseState(IDialogMachine machine, IBubbleDialogController controller) : base(machine)
        {
            _controller = controller;
        }

        public void Enter()
        {
            _controller.Current.SetActive(false);
            if(_controller.CurrentNode != null)
                _machine.ChangeState<BubbleDialogOpenState>();
            else 
                _machine.ChangeState<BubbleDialogEmptyState>();
        }
    }
}