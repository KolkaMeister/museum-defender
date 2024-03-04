using Dialogs.States;
using Infrastructure.Timers;
using UnityEngine;

namespace Dialogs.Sideline.States
{
    public class BubbleDialogWaitState : BubbleDialogState, IEnterState, IUpdateState, IExitState
    {
        private readonly BubbleDialogController _controller;
        private Timer _waitTime;
        
        public BubbleDialogWaitState(IDialogMachine machine, BubbleDialogController controller) : base(machine)
        {
            _controller = controller;
        }

        public void Enter()
        {
            TimerManager.AddTimer(_waitTime = _controller.Current.WaitTime);
        }

        public void Update()
        {
            if (_waitTime.TimeLeft <= 0)
            {
                _machine.ChangeState<BubbleDialogCloseState>();
            }
        }

        public void Exit()
        {
            _controller.Current.SetActive(false);
        }
    }
}