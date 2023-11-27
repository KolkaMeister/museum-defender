using System;
using Dialogs.Nodes;
using Infrastructure;

namespace Dialogs.Sideline
{
    public class BubbleDialogSystem : IBubbleDialogSystem
    {
        private readonly IBubbleDialogManager _manager;
        private readonly ICoroutineRunner _runner;
        private readonly IBubbleDialogController _controller;

        public DialogTree CurrentDialog { get; private set; }
        public DialogNode CurrentNode { get; private set; }
        
        public event Action OnDialogStarted;
        public event Action OnDialogEnded;
        public event Action OnPhraseStarted;

        public BubbleDialogSystem(IBubbleDialogManager manager, ICoroutineRunner runner)
        {
            _manager = manager;
            _runner = runner;
            _controller = new BubbleDialogController(this, _runner);
        }

        public void Start(Character initiator, Character initiated, DialogTree dialog)
        {
            CurrentDialog = dialog;
            OnDialogStarted?.Invoke();
            _controller.Start(new SpeakerData(CurrentDialog.Speakers[0], initiator.DialogView), 
                new SpeakerData(CurrentDialog.Speakers[1], initiated.DialogView));
        }

        public DialogNode GetNextNode()
        {
            CurrentNode = CurrentNode == null ? CurrentDialog.Root : CurrentNode.Child;
            if (CurrentNode == null)
            {
                Finish();
            }
            else
            {
                CurrentNode.OnPhraseStarted?.Invoke();
                OnPhraseStarted?.Invoke();
            }

            return CurrentNode;
        }


        public void Finish()
        {
            _controller.Stop();
            OnDialogEnded?.Invoke();
            
            CurrentNode = null;
            CurrentDialog = null;
            _manager.RemoveDialog(this);
        }
    }

    public interface IBubbleDialogSystem
    {
        public DialogTree CurrentDialog { get; }
        public DialogNode CurrentNode { get; }
        
        public event Action OnDialogStarted;
        public event Action OnDialogEnded;
        public event Action OnPhraseStarted;

        public void Start(Character initiator, Character initiated, DialogTree dialog);
        public DialogNode GetNextNode();
        public void Finish();
    }
}