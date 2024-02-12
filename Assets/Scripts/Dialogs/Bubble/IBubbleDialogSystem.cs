using System;
using Dialogs.Nodes;

namespace Dialogs.Sideline
{
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