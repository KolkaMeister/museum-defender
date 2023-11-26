using System;
using Dialogs.Nodes;

namespace Dialogs
{
    public interface IDialogSystem
    {
        public DialogTree CurrentDialog { get; }
        public DialogNode CurrentNode { get; }
        
        public event Action OnDialogStarted;
        public event Action OnDialogEnded;
        public event Action OnPhraseStarted;
        
        public void SetController(DialogController controller);
        public void Start(DialogTree tree);
    }
}