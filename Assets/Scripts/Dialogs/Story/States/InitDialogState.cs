using Infrastructure.Timers;
using UnityEngine;

namespace Dialogs.States
{
    public class InitDialogState : DialogState, IEnterState, IUpdateState, IExitState
    {
        private static readonly int _openKey = Animator.StringToHash("Open");
        private Timer _animationTimer;

        public InitDialogState(DialogMachine machine, DialogBox box) : base(machine, box)
        {
        }

        public void Enter()
        {
            _box.gameObject.SetActive(true);
            _box.Clear();
            _box.Animator.SetTrigger(_openKey);
            TimerManager.AddTimer(_animationTimer = _box.OpenTime);
        }

        public void Update()
        {
            if (_animationTimer <= 0)
                _machine.ChangeState<StartPhraseState>();
        }

        public void Exit()
        {
            _box.Button.enabled = true;
        }
    }
}