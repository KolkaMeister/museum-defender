using Dialogs.Nodes;
using Dialogs.States;

namespace Dialogs.Sideline.States
{
    public class BubbleDialogTypeState : BubbleDialogState, IEnterState, IUpdateState
    {
        private readonly IBubbleDialogController _controller;

        public BubbleDialogTypeState(IDialogMachine machine, IBubbleDialogController controller) : base(machine)
        {
            _controller = controller;
        }


        public void Enter()
        {
            DialogNode node = _controller.CurrentNode;
            if (node == null)
                _machine.ChangeState<BubbleDialogEmptyState>();
            else
            {
                BubbleDialogView view = _controller.Current;
                view.SetActive(true);
                view.Text.SetText(node.Text, true);
            }
        }

        public void Update()
        {
            if (!_controller.Current.Text.IsTyping())
            {
                _machine.ChangeState<BubbleDialogWaitState>();
            }
        }
    }
}