using Infrastructure.Timers;
using UnityEngine;

namespace Dialogs.States
{
    public class CloseDialogState : DialogState, IEnterState, IUpdateState, IExitState
    {
        private static readonly int _closeKey = Animator.StringToHash("Close");
        private Timer _animationTimer;

        public CloseDialogState(DialogMachine machine, DialogBox box) : base(machine, box)
        {
        }

        public void Enter()
        {
            _box.Clear();
            _box.Button.enabled = false;
            _box.Animator.SetTrigger(_closeKey);
            TimerManager.AddTimer(_animationTimer = _box.CloseTime);
        }
        
        public void Update()
        {
            if (_animationTimer <= 0)
            {
                _machine.ChangeState<WaitDialogState>();
            }
        }

        public void Exit()
        {
            _box.gameObject.SetActive(false);
        }
    }
}