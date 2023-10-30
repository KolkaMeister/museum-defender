using Dialogs.States;

namespace Dialogs.Sideline.States
{
    public class BubbleDialogOpenState : BubbleDialogState, IEnterState
    {
        private readonly IBubbleDialogController _controller;

        public BubbleDialogOpenState(IDialogMachine machine, IBubbleDialogController controller) : base(machine)
        {
            _controller = controller;
        }

        public void Enter()
        {
            if (_controller.ReadNextNode() == null)
            {
                _machine.ChangeState<BubbleDialogEmptyState>();
            }
            else
            {
                _controller.Current.SetActive(true);
                _machine.ChangeState<BubbleDialogTypeState>();
            }
        }
    }
}