using System;
using System.Collections.Generic;
using Dialogs.Nodes;
using Dialogs.States;
using JetBrains.Annotations;
using UnityEngine.Events;
using Zenject;

namespace Dialogs
{
    [UsedImplicitly]
    public class DialogController : IDialogController, ITickable, IInitializable, IDisposable
    {
        private readonly DialogBox _box;
        private readonly DialogSystem _dialogSys;
        private readonly DialogMachine _machine;

        private readonly List<UnityAction> _answerDelegates = new List<UnityAction>();

        public DialogController(DialogBox box, DialogSystem dialogSys)
        {
            _box = box;
            _dialogSys = dialogSys;
            _machine = new DialogMachine();
            _machine.AddState(new InitDialogState(_machine, _box));
            _machine.AddState(new StartPhraseState(_machine, _box));
            _machine.AddState(new TypePhraseState(_machine, _box, this));
            _machine.AddState(new WaitDialogState(_machine, _box));
            _machine.AddState(new CloseDialogState(_machine, _box));
        }

        public void Initialize()
        {
            _box.Button.onClick.AddListener(OnClickedCallback);
            
            var list = _box.AnswerGroup.GetItems();
            for (var i = 0; i < list.Count; i++)
            {
                int localIndex = i;
                _answerDelegates.Add(() => _dialogSys.Next(localIndex));
                list[i].Button.onClick.AddListener(_answerDelegates[i]);
            }
        }

        public void Dispose()
        {
            _box.Button.onClick.RemoveListener(OnClickedCallback);
            
            var list = _box.AnswerGroup.GetItems();
            for (var i = 0; i < list.Count; i++)
            {
                list[i].Button.onClick.RemoveListener(_answerDelegates[i]);
            }
        }

        public void Start()
        {
            _machine.ChangeState<InitDialogState>();
        }

        public DialogNode GetNextNode()
        {
            return _dialogSys.GetNextNode();
        }

        public void TypeNext()
        {
            _machine.ChangeState<StartPhraseState>();
        }

        public void Finish()
        {
            _machine.ChangeState<CloseDialogState>();
        }

        public void Tick()
        {
            _machine.Update();
        }

        private void OnClickedCallback()
        {
            if (_machine.GetCurrentState is TypePhraseState)
                _machine.ChangeState<WaitDialogState>();
            else
                _dialogSys.Next(-1);
        }
    }

    public interface IDialogController
    {
        public void Start();
        public DialogNode GetNextNode();
        public void TypeNext();
        public void Finish();
    }
}