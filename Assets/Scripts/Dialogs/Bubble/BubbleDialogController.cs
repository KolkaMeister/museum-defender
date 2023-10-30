using System.Collections;
using System.Collections.Generic;
using Dialogs.Nodes;
using Dialogs.Sideline.States;
using Dialogs.States;
using Infrastructure;

namespace Dialogs.Sideline
{
    public class BubbleDialogController : IBubbleDialogController
    {
        private readonly IBubbleDialogSystem _system;
        private readonly ICoroutineRunner _runner;
        private readonly IDialogMachine _machine;
        private readonly List<SpeakerData> _speakers = new List<SpeakerData>(2);

        private BubbleDialogView _current;
        private DialogNode _currentNode;

        public BubbleDialogView Current => _current;
        public DialogNode CurrentNode => _currentNode;

        public BubbleDialogController(IBubbleDialogSystem system, ICoroutineRunner runner)
        {
            _system = system;
            _runner = runner;
            _machine = new DialogMachine();
            _machine.AddState(new BubbleDialogEmptyState(_machine));
            _machine.AddState(new BubbleDialogOpenState(_machine, this));
            _machine.AddState(new BubbleDialogTypeState(_machine, this));
            _machine.AddState(new BubbleDialogWaitState(_machine, this));
            _machine.AddState(new BubbleDialogCloseState(_machine, this));
        }

        public void Start(SpeakerData initiator, SpeakerData initiated)
        {
            _speakers.Add(initiator);
            _speakers.Add(initiated);
            _machine.ChangeState<BubbleDialogOpenState>();
            _runner.RunCoroutine(Update());
        }

        public DialogNode ReadNextNode()
        {
            _currentNode = _system.GetNextNode();
            _current = _speakers.Find(x => x.Name == _currentNode.Name).Speaker;
            return _currentNode;
        }

        public void Stop()
        {
            _currentNode = null;
            _machine.ChangeState<BubbleDialogCloseState>();
            _current = null;
            _speakers.Clear();
        }

        private IEnumerator Update()
        {
            while (true)
            {
                yield return null;
                _machine.Update();
            }
        }
    }

    public interface IBubbleDialogController
    {
        public BubbleDialogView Current { get; }
        public DialogNode CurrentNode { get; }

        public void Start(SpeakerData initiator, SpeakerData initiated);
        public DialogNode ReadNextNode();
        public void Stop();
    }
}