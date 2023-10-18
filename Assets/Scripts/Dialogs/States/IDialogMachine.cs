using Dialogs.Nodes;

namespace Dialogs.States
{
    public interface IDialogMachine
    {
        public void StartDialog();
        
        public void ChangeState<TState>()
            where TState : DialogState;

        public void TypeNext();
        public void ForceType();
        public DialogNode GetPhrase();

        public void Update();

        public void Finish();
    }
}