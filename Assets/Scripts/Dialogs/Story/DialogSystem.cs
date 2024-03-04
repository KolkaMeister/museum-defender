using System;
using Dialogs.Nodes;

namespace Dialogs
{
    public class DialogSystem : IDialogSystem
    {
        private DialogController _controller;

        public DialogTree CurrentDialog { get; private set; }
        public DialogNode CurrentNode { get; private set; }
        
        public event Action OnDialogStarted;
        public event Action OnDialogEnded;
        public event Action OnPhraseStarted;

        public void Start(DialogTree tree)
        {
            if (_controller == null) return;

            CurrentDialog = tree;
            CurrentNode = tree.Root;
            _controller.Start();
            OnDialogStarted?.Invoke();
        }

        public void SetController(DialogController controller)
        {
            _controller = controller;
        }

        public DialogNode GetNextNode()
        {
            CurrentNode.OnPhraseStarted?.Invoke();
            OnPhraseStarted?.Invoke();
            return CurrentNode;
        }

        public void Next(int index)
        {
            if (CurrentNode is not BranchDialogNode branch)
            {
                CurrentNode = CurrentNode.Child;
            }
            else
            {
                if (index == -1) return;
                branch.Answers[index].OnPhraseStarted?.Invoke();
                CurrentNode = branch.Answers[index].Child;
            }

            if (CurrentNode == null)
            {
                FinishDialog();
                return;
            }

            _controller.TypeNext();
        }

        private void FinishDialog()
        {
            OnDialogEnded?.Invoke();
            _controller.Finish();
            CurrentNode = null;
            CurrentDialog = null;
        }
    }
}