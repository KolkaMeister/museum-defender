using System;
using Dialogs.Nodes;

namespace Dialogs
{
    public class DialogSystem : IDialogSystem
    {
        public event Action OnDialogStarted;
        public event Action OnDialogEnded;
        public event Action OnPhraseStarted;

        private DialogController _controller;
        private DialogTree _currentDialog;
        private DialogNode _currentNode;

        private bool _isSpeaking;

        public void Start(DialogTree tree)
        {
            if (_controller == null) return;

            _currentDialog = tree;
            _currentNode = tree.Root;
            _controller.Start();
            OnDialogStarted?.Invoke();
        }

        public void SetController(DialogController controller)
        {
            _controller = controller;
        }

        public DialogNode GetNextNode()
        {
            OnPhraseStarted?.Invoke();
            _currentNode.OnPhraseStarted?.Invoke();
            return _currentNode;
        }

        public void Next(int index)
        {
            if (_currentNode is not BranchDialogNode branch)
            {
                _currentNode = _currentNode.Child;
            }
            else
            {
                if (index == -1) return;
                branch.Answers[index].OnPhraseStarted?.Invoke();
                _currentNode = branch.Answers[index].Child;
            }

            if (_currentNode == null)
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
            _currentNode = null;
            _currentDialog = null;
        }
    }

    public interface IDialogSystem
    {
        public void SetController(DialogController controller);
        public void Start(DialogTree tree);
    }
}